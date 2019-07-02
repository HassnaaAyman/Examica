using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI.Examica.API.Dtos;
using UI.Examica.Model.Core;
using UI.Examica.Model.Core.Domains;
using UI.Examica.Model.Helpers;

namespace UI.Examica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoolsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;

        public PoolsController(IUnitOfWork _unitOfWork, UserManager<AppUser> _userManager)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;
        }

        // GET: api/pools/1
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PoolDto>> GetQuestionsOfOrg(int id)
        {
            Organization organization = await unitOfWork.Organizations.GetById(id);
            if (organization == null) return NotFound();
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (!(user.IsOwnerOfOrg(id) || user.IsExaminerOfOrg(id) || user.IsExamineeOfOrg(id))) return Forbid();
            IEnumerable<Question> questions = await unitOfWork.Questions.GetQuestionsOfOrg(id);
            IEnumerable<ComplexQuestion> complexQuestions = await unitOfWork.ComplexQuestions.GetComplexsWithSubByOrgId(id);
            PoolDto pool = new PoolDto
            {
                Questions = Mapper.Map<IEnumerable<Question>, List<QuestionDto>>(questions),
                ComplexQuestions = Mapper.Map<IEnumerable<ComplexQuestion>, List<ComplexQuestionDto>>(complexQuestions)
            };
            return Ok(pool);
        }
    }
}