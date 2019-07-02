using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI.Examica.API.Dtos;
using UI.Examica.API.Helpers;
using UI.Examica.Model.Core;
using UI.Examica.Model.Core.Domains;
using UI.Examica.Model.Helpers;
using UI.Examica.Model.Persistence;

namespace UI.Examica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        #region properties
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;
        #endregion

        #region constructor

        public OrganizationsController(IUnitOfWork _unitOfWork, UserManager<AppUser> _userManager)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;
        }
        #endregion

        #region Get Organizations
        // GET: api/Organizations
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizations()
        {
            return Ok(Mapper.Map<List<OrganizationDto>>(await unitOfWork.Organizations.GetAll()));
        }
        #endregion

        #region Get Organizations By User
        // GET: api/Organizations
        [HttpGet("user")]

        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizationsOfUser()
        {
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            List<OrganizationDto> organizations = new List<OrganizationDto>();
            organizations.AddRange(Mapper.Map<List<OrganizationDto>>(user.Organizations));
            organizations.AddRange(Mapper.Map<List<OrganizationDto>>(user.OrganizationAdmins));
            organizations.AddRange(Mapper.Map<List<OrganizationDto>>(user.OrganizationExaminers));
            organizations.AddRange(Mapper.Map<List<OrganizationDto>>(user.OrganizationObservers));
            OrganizationDtoComparer comparer = new OrganizationDtoComparer();
            return Ok(organizations.Distinct(comparer));
        }
        #endregion

        #region Get organization by id
        // GET: api/Organizations/5
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Organization>> GetOrganization(int id)
        {
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (user.IsOwnerOfOrg(id) || user.IsAdminOfOrg(id) || user.IsExaminerOfOrg(id))
            {
                var organization = await unitOfWork.Organizations.GetById(id);

                if (organization == null)
                {
                    return NotFound();
                }

                return Ok(Mapper.Map<OrganizationDto>(organization));
            }
            else return Unauthorized("You are not an owner!");
        }
        #endregion

        #region  Get Organization Admins
        // GET: api/Organizations/OrganizationAdmins/5
        //[HttpGet("{id}")]
        [HttpGet("OrganizationAdmins/{id}")]
        public async Task<ActionResult<OrganizationAdmin>> GetOrganizationAdmins(int id)
        {
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (user.IsOwnerOfOrg(id))
            {
                var Admins = await unitOfWork.OrganizationAdmins.Find(org => org.OrgnaizationId == id);

                if (Admins == null)
                {
                    return NotFound();
                }

                return Ok(Admins);
            }
            else return Unauthorized("You are not an owner!");
        }
        #endregion

        #region  GetOrganizationExaminees
        // GET: api/Organizations/OrganizationExaminees/5
        [HttpGet("OrganizationExaminees/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<OrganizationAdmin>> GetOrganizationExaminees(int id)
        {

            var Admins = await unitOfWork.organizationExaminees.Find(org => org.OrgnaizationId == id);

            if (Admins == null)
            {
                return NotFound();
            }

            return Ok(Admins);

        }

        #endregion

        #region  GetOrganizationExaminers
        // GET: api/Organizations/OrganizationExaminers/5
        [HttpGet("OrganizationExaminers/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OrganizationAdmin>> GetOrganizationExaminers(int id)
        {

            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (user.IsOwnerOfOrg(id) || user.IsAdminOfOrg(id))
            {
                var Admins = await unitOfWork.OrganizationExaminers.Find(org => org.OrgnaizationId == id);

                if (Admins == null)
                {
                    return NotFound();
                }

                return Ok(Admins);

            }
            else return Unauthorized("You are not authorized");

        }

        #endregion

        #region  GetOrganizationAdmObservers
        // GET: api/Organizations/OrganizationExaminers/5
        [HttpGet("OrganizationObservers/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OrganizationAdmin>> GetOrganizationObservers(int id)
        {
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (!user.IsExaminerOfOrg(id))
            {
                var Admins = await unitOfWork.OrganizationObservers.Find(org => org.OrgnaizationId == id);

                if (Admins == null)
                {
                    return NotFound();
                }

                return Ok(Admins);

            }
            else return Unauthorized("You are not authorized");
        }

        #endregion

        #region Edit Organization
        // PUT: api/Organizations/5
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> PutOrganization(int id, [FromBody] OrganizationDto organization)
        {
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (user.IsOwnerOfOrg(id) || user.IsAdminOfOrg(id))
            {
                var org = await unitOfWork.Organizations.GetById(id);
                if (id != organization.Id)
                {
                    return BadRequest();
                }
                else if (org == null)
                {
                    NotFound();
                }
                else
                {
                    try
                    {
                        var mappedOrg = Mapper.Map<OrganizationDto, Organization>(organization);
                        await unitOfWork.SaveAsync();
                        return Ok(mappedOrg);

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await OrganizationExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                }
                return NoContent();

            }

            else return Unauthorized("You are not an owner/admin of this organinzation");
        }

        #endregion

        #region Add Organization
        // POST: api/Organizations
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Organization>> PostOrganization(Organization organization)
        {
            AppUser user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized("You are not authorized");
            await unitOfWork.Organizations.Add(organization);
            int res = await unitOfWork.SaveAsync();
            if (res > 0) return Ok(Mapper.Map<OrganizationDto>(organization));
            else return BadRequest();
        }

        #endregion

        #region Delete Organization
        // DELETE: api/Organizations/5
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Organization>> DeleteOrganization(int id)
        {
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (user.IsOwnerOfOrg(id))
            {
                var organization = await unitOfWork.Organizations.GetById(id);
                if (organization == null)
                {
                    return NotFound();
                }
                else
                {
                    unitOfWork.Organizations.Remove(organization);
                    await unitOfWork.SaveAsync();

                }

                return organization;
            }
            else return Unauthorized("You are not an owner of this organinzation");
        }

        #endregion

        public async Task<bool> OrganizationExists(int id)
        {
            return await unitOfWork.Organizations.IsExistedAsync(org => org.Id == id);
        }


    }

}
