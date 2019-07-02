using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI.Examica.API.Dtos;
using UI.Examica.API.Helpers;
using UI.Examica.Model.Core;
using UI.Examica.Model.Core.Domains;
using UI.Examica.Model.Helpers;

namespace UI.Examica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;

        public UsersController(IUnitOfWork _unitOfWork, UserManager<AppUser> _userManager)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;
        }

        // GET: api/users
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            IEnumerable<AppUser> users = await unitOfWork.AppUsers.GetAll();
            return Ok(Mapper.Map<List<UserDto>>(users));
        }

        [HttpGet("organization/{orgId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersOfOrg(int orgId)
        {
            Organization org = await unitOfWork.Organizations.GetOrganizationWithUsers(orgId);
            List<UserDto> users = new List<UserDto> { Mapper.Map<UserDto>(org.Owner) };
            users.AddRange(Mapper.Map<List<UserDto>>(org.OrganizationAdmins));
            users.AddRange(Mapper.Map<List<UserDto>>(org.OrganizationExaminers));
            users.AddRange(Mapper.Map<List<UserDto>>(org.OrganizationExaminees));
            users.AddRange(Mapper.Map<List<UserDto>>(org.OrganizationObservers));
            UserDtoComparer comparer = new UserDtoComparer();
            return Ok(users.Distinct(comparer));
        }

        // GET: api/users/1/1
        [HttpGet("{userId}/{orgId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UserDto>> GetUser(string userId, int orgId)
        {
            AppUser user = unitOfWork.AppUsers.GetUserWithOrgs(userId);
            if (user == null) return NotFound();
            UserDto userDto = Mapper.Map<UserDto>(user);
            userDto.Roles = new RolesDto
            {
                UserId = user.Id,
                OrganizationId = orgId,
                IsOwner = user.IsOwnerOfOrg(orgId),
                IsAdmin = user.IsAdminOfOrg(orgId),
                IsExaminer = user.IsExaminerOfOrg(orgId),
                IsExaminee = user.IsExamineeOfOrg(orgId),
                IsObserver = user.IsObserverOfOrg(orgId)
            };
            return Ok(userDto);
        }

        // GET: api/users/1
        [HttpGet("{userId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserById(string userId)
        {
            AppUser user = await unitOfWork.AppUsers.GetById(userId);
            if (user == null) return NotFound();
            UserDto userDto = Mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        // POST: api/users/assign
        [Authorize]
        [HttpPost("assign")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> AssignUser([FromBody] RolesDto Roles)
        {
            AppUser owner = await userManager.GetUserAsync(User);
            owner = unitOfWork.AppUsers.GetUserWithOrgs(owner.Id);
            if (!(owner.IsOwnerOfOrg(Roles.OrganizationId) || owner.IsAdminOfOrg(Roles.OrganizationId))) return Forbid();
            AppUser user = unitOfWork.AppUsers.GetUserWithOrgs(Roles.UserId);
            if(user == null) return BadRequest();
            if(! await unitOfWork.Organizations.IsExistedAsync(org => org.Id == Roles.OrganizationId)) return BadRequest();
            bool isAdmin = user.IsAdminOfOrg(Roles.OrganizationId);
            bool isExaminer = user.IsExaminerOfOrg(Roles.OrganizationId);
            bool isExaminee = user.IsExamineeOfOrg(Roles.OrganizationId);
            bool isObserver = user.IsObserverOfOrg(Roles.OrganizationId);
            if (Roles.IsAdmin && !isAdmin) {
                await unitOfWork.OrganizationAdmins.Add(new OrganizationAdmin { AppUserId = Roles.UserId, OrgnaizationId = Roles.OrganizationId });
            }
            else if (!Roles.IsAdmin && isAdmin) {
                var role = await unitOfWork.OrganizationAdmins.GetById(Roles.UserId, Roles.OrganizationId);
                unitOfWork.OrganizationAdmins.Remove(role);
            }
            if (Roles.IsExaminer && !isExaminer)
            {
                await unitOfWork.OrganizationExaminers.Add(new OrganizationExaminer { AppUserId = Roles.UserId, OrgnaizationId = Roles.OrganizationId });
            }
            else if (!Roles.IsExaminer && isExaminer) {
                var role = await unitOfWork.OrganizationExaminers.GetById(Roles.UserId, Roles.OrganizationId);
                unitOfWork.OrganizationExaminers.Remove(role);
            }
            if (Roles.IsExaminee && !isExaminee)
            {
                await unitOfWork.organizationExaminees.Add(new OrganizationExaminee { AppUserId = Roles.UserId, OrgnaizationId = Roles.OrganizationId });
            }
            else if (!Roles.IsExaminee && isExaminee) {
                var role = await unitOfWork.organizationExaminees.GetById(Roles.UserId, Roles.OrganizationId);
                unitOfWork.organizationExaminees.Remove(role);
            }
            if (Roles.IsObserver && !isObserver)
            {
                await unitOfWork.OrganizationObservers.Add(new OrganizationObserver { AppUserId = Roles.UserId, OrgnaizationId = Roles.OrganizationId });
            }
            else if (!Roles.IsObserver && isObserver) {
                var role = await unitOfWork.OrganizationObservers.GetById(Roles.UserId, Roles.OrganizationId);
                unitOfWork.OrganizationObservers.Remove(role);
            }
            await unitOfWork.SaveAsync();
            return Ok("Assigned");
        }
    }
}