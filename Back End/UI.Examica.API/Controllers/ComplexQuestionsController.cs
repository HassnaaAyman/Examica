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
using UI.Examica.Model.Core;
using UI.Examica.Model.Core.Domains;
using UI.Examica.Model.Helpers;

namespace UI.Examica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComplexQuestionsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;

        public ComplexQuestionsController(IUnitOfWork _unitOfWork, UserManager<AppUser> _userManager)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;
        }

        // GET: api/complexquestions/1
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ComplexQuestionDto>> GetQuestion(int id)
        {
            ComplexQuestion question = await unitOfWork.ComplexQuestions.GetComplexWithSubById(id);
            if (question == null) return NotFound();
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (!question.IsPublic && !(user.IsOwnerOfOrg(question.OrganizationId) || user.IsExaminerOfOrg(question.OrganizationId) || user.IsExamineeOfOrg(question.OrganizationId))) return Forbid();
            //return Ok(question);
            return Ok(Mapper.Map<ComplexQuestionDto>(question));
        }

        // GET: api/complexquestions/organization/1
        [HttpGet("organization/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<ComplexQuestionDto>>> GetQuestionsOfOrg(int id)
        {
            Organization organization = await unitOfWork.Organizations.GetById(id);
            if (organization == null) return NotFound();
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (!(user.IsOwnerOfOrg(id) || user.IsExaminerOfOrg(id) || user.IsExamineeOfOrg(id))) return Forbid();
            IEnumerable<ComplexQuestion> questions = await unitOfWork.ComplexQuestions.GetComplexsWithSubByOrgId(id);
            return Ok(Mapper.Map<IEnumerable<ComplexQuestion>, List<ComplexQuestionDto>>(questions));
        }

        // DELETE: api/complexquestions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ComplexQuestionDto>> DeleteComplexQuestion(int id)
        {
            ComplexQuestion complexQuestion = await unitOfWork.ComplexQuestions.SingleOrDefault(cq => cq.Id == id);
            if (complexQuestion == null) return BadRequest();
            var orgId = complexQuestion.OrganizationId;
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (user == null) return Unauthorized();
            if (!(user.IsObserverOfOrg(orgId) || user.IsExaminerOfOrg(orgId) || user.IsAdminOfOrg(orgId))) return Forbid();
            unitOfWork.ComplexQuestions.Remove(complexQuestion);
            return Ok(await unitOfWork.SaveAsync());
        }

        // POST: api/complexquestions
        [HttpPost]
        public async Task<ActionResult<ComplexQuestionDto>> PostComplexQuestion([FromBody] ComplexQuestionDto complexQuestionDto)
        {
            int orgId = complexQuestionDto.OrganizationId;
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (!(user.IsOwnerOfOrg(orgId) || user.IsExaminerOfOrg(orgId) || user.IsAdminOfOrg(orgId))) return Forbid();
            complexQuestionDto.Id = 0;
            ComplexQuestion comp = Mapper.Map<ComplexQuestion>(complexQuestionDto);
            await unitOfWork.ComplexQuestions.Add(comp);
            await unitOfWork.SaveAsync();
            return Ok(Mapper.Map<ComplexQuestionDto>(comp));
        }

        // PUT: api/complexquestions/questions
        [HttpPut("questions")]
        [Authorize]
        public async Task<IActionResult> PutExamQuestions(ComplexQuestionsQuestionsDto dto)
        {
            ComplexQuestion comp = await unitOfWork.ComplexQuestions.GetById(dto.ComplexQuestionId);
            if (comp == null) return BadRequest();
            var orgId = comp.OrganizationId;
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (!(user.IsOwnerOfOrg(orgId) || user.IsExaminerOfOrg(orgId) || user.IsAdminOfOrg(orgId))) return Forbid();
            unitOfWork.QuestionComplexQuestions.RemoveRange(await unitOfWork.QuestionComplexQuestions.Find(qcq => qcq.ComplexQuestionId == comp.Id));
            await unitOfWork.SaveAsync();
            List<QuestionComplexQuestion> questions = new List<QuestionComplexQuestion>();
            foreach (int quesId in dto.QuestionsIds)
            {
                Question q = await unitOfWork.Questions.GetById(quesId);
                if (q == null) return BadRequest();
                if (q.IsPublic || q.OrganizationId == comp.OrganizationId)
                    questions.Add(new QuestionComplexQuestion { ComplexQuestionId = comp.Id, QuestionId = quesId });
            };
            await unitOfWork.QuestionComplexQuestions.AddRange(questions);
            await unitOfWork.SaveAsync();
            return Ok(Mapper.Map<ComplexQuestionDto>(await unitOfWork.ComplexQuestions.GetComplexWithSubById(dto.ComplexQuestionId)));
        }
    }
}