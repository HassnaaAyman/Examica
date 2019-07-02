using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI.Examica.API.Dtos;
using UI.Examica.Model.Core;
using UI.Examica.Model.Core.Domains;
using UI.Examica.Model.Helpers;
using UI.Examica.Model.Persistence;

namespace UI.Examica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;

        public QuestionsController(IUnitOfWork _unitOfWork, UserManager<AppUser> _userManager)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;
        }

        // GET: api/Questions
        [Authorize(Roles = "Developer")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<QuestionDto>>> GetQuestions()
        {
            IEnumerable<Question> questions = await unitOfWork.Questions.GetQuestionsWithOptions();
            List<QuestionDto> questionDtos = Mapper.Map<IEnumerable<Question>, List<QuestionDto>>(questions);
            return Ok(questionDtos);
        }

        // GET: api/questions/1
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<QuestionDto>> GetQuestion(int id)
        {
            Question question = await unitOfWork.Questions.GetById(id);
            if (question == null) return NotFound();
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (!question.IsPublic && !(user.IsOwnerOfOrg(question.OrganizationId) || user.IsExaminerOfOrg(question.OrganizationId) || user.IsExamineeOfOrg(question.OrganizationId))) return Forbid();
            return Ok(Mapper.Map<QuestionDto>(await unitOfWork.Questions.GetQuestionWithOptionsById(id)));
        }

        // GET: api/Questions/organization/1
        [HttpGet("organization/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<QuestionDto>>> GetQuestionsOfOrg(int id)
        {
            Organization organization = await unitOfWork.Organizations.GetById(id);
            if (organization == null) return NotFound();
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (!(user.IsOwnerOfOrg(id) || user.IsExaminerOfOrg(id) || user.IsExamineeOfOrg(id))) return Forbid();
            IEnumerable<Question> questions = await unitOfWork.Questions.GetQuestionsOfOrg(id);
            List<QuestionDto> questionDtos = Mapper.Map<IEnumerable<Question>, List<QuestionDto>>(questions);
            return Ok(questionDtos);
        }

        // DELETE: api/questions/1
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<QuestionDto>> DeleteQuestion(int id)
        {
            Question question = await unitOfWork.Questions.GetById(id);
            if (question == null) return NotFound();
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (!(user.IsOwnerOfOrg(question.OrganizationId) || user.IsExaminerOfOrg(question.OrganizationId) || user.IsExamineeOfOrg(question.OrganizationId))) return Forbid();
            unitOfWork.Questions.Remove(question);
            unitOfWork.QuestionOptions.RemoveRange(await unitOfWork.QuestionOptions.Find(qo => qo.QuestionId == question.Id));
            unitOfWork.QuestionComplexQuestions.RemoveRange(await unitOfWork.QuestionComplexQuestions.Find(qo => qo.QuestionId == question.Id));
            unitOfWork.ExamQuestions.RemoveRange(await unitOfWork.ExamQuestions.Find(qo => qo.QuestionId == question.Id));
            await unitOfWork.SaveAsync();
            return Ok(new { questionId= question.Id});
        }
        // POST: api/questions
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<QuestionDto>> PostQuestion([FromBody]AddQuestionDto questionDto)
        {
            int id = questionDto.OrganizationId;
            Organization organization = await unitOfWork.Organizations.GetById(id);
            if (organization == null) return BadRequest();
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (!(user.IsOwnerOfOrg(id) || user.IsExaminerOfOrg(id) || user.IsExamineeOfOrg(id))) return Forbid();
            IEnumerable<Option> options = questionDto.Options;
            Question question = Mapper.Map<Question>(questionDto);
            question.QuestionOptions = new List<QuestionOption>();
            foreach (Option item in options)
            {
                Option option = await unitOfWork.Options.SingleOrDefault(o => o.Name == item.Name);
                if(option == null)
                {
                    option = item;
                    await unitOfWork.Options.Add(option);
                }
                await unitOfWork.SaveAsync();
                question.QuestionOptions.Add(new QuestionOption { OptionId = item.Id , Option = option});
            }
            await unitOfWork.Questions.Add(question);
            if (await unitOfWork.SaveAsync() < 1) return BadRequest();
            else return Ok(Mapper.Map<QuestionDto>(question));
        }

        // Put: api/questions/1
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<QuestionDto>> UpdateQuestion(int id, [FromBody]AddQuestionDto questionDto)
        {
            Question question = await unitOfWork.Questions.GetById(id);
            if (question == null) return NotFound();
            Organization organization = await unitOfWork.Organizations.GetById(question.OrganizationId);
            if (organization == null) return BadRequest();
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (!(user.IsOwnerOfOrg(organization.Id) || user.IsExaminerOfOrg(organization.Id) || user.IsExamineeOfOrg(organization.Id))) return Forbid();
            unitOfWork.QuestionOptions.RemoveRange(await unitOfWork.QuestionOptions.Find(qo => qo.QuestionId == question.Id));
            IEnumerable<Option> options = questionDto.Options;
            Mapper.Map(questionDto, question);
            question.QuestionOptions = new List<QuestionOption>();
            foreach (Option item in options)
            {
                Option option = await unitOfWork.Options.SingleOrDefault(o => o.Name == item.Name);
                if (option == null)
                {
                    option = item;
                    await unitOfWork.Options.Add(option);
                }
                await unitOfWork.SaveAsync();
                question.QuestionOptions.Add(new QuestionOption { OptionId = item.Id, Option = option });
            }
            unitOfWork.Questions.Update(question);
            if (await unitOfWork.SaveAsync() < 1) return BadRequest();
            else return Ok(Mapper.Map<QuestionDto>(question));
        }
    }
}
