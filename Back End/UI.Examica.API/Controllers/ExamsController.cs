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
using UI.Examica.Model.Core;
using UI.Examica.Model.Core.Domains;
using UI.Examica.Model.Helpers;
using UI.Examica.Model.Persistence;

namespace UI.Examica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExamsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;

        public ExamsController(IUnitOfWork _unitOfWork , UserManager<AppUser> _userManager)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;

        }

        // GET: api/Exams
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Developer")]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExams()
        {
            return Ok(Mapper.Map<List<ExamDto>>(await unitOfWork.Exams.GetAll()));
        }

        // GET: api/Exams/assign/1/1
        [HttpGet("assign/{examId}/{quesId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> Assign(int examId, int quesId)
        {
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            Exam exam = await unitOfWork.Exams.GetById(examId);
            if (!user.IsExaminerOfOrg(exam.OrganizationId)) return Forbid();
            await unitOfWork.ExamQuestions.Add(new ExamQuestion { ExamId= examId, QuestionId= quesId});
            await unitOfWork.SaveAsync();
            return Ok(true);
        }

        // GET: api/Examinee
        [HttpGet("examinee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExamsByExaminee()
        {
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            List<OrganizationExaminee> organizations = user.OrganizationExaminees;
            List<Exam> exams = new List<Exam>();
            foreach (OrganizationExaminee orgE  in organizations)
            {
                exams.AddRange(await unitOfWork.Exams.Find(e => e.OrganizationId == orgE.OrgnaizationId));
            }
            return Ok(Mapper.Map<List<ExamDto>>(exams));
        }

        // GET: api/Exams/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult<Exam>> GetExam(int id)
        {
            var exam = await unitOfWork.Exams.GetExamWithQuestions(id);
            if (exam == null) return NotFound();
            var orgId = exam.OrganizationId;
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (exam.IsPublic || user.IsOwnerOfOrg(orgId) || user.IsExaminerOfOrg(orgId) || user.IsExamineeOfOrg(orgId))
            {
                return Ok(Mapper.Map<ExamDto>(exam));
            }
            else
            {
                return Unauthorized("You  are not Authorized");
            }
        }

        [HttpGet("organization/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<ActionResult<Exam>> GetExamByOrganizationId(int id)
        {
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
           
            if (user.IsAdminOfOrg(id) || user.IsExaminerOfOrg(id) || user.IsAdminOfOrg(id))
            {       
                var exams = await unitOfWork.Exams.Find(e => e.OrganizationId == id );   
                if (exams == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Mapper.Map<List<ExamDto>>(exams));

                }
            }
            else
            {
                return Unauthorized("You  are not authorized");
            }

        }

        // PUT: api/Exams/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutResult(int id, ExamDto examDto)
        {
            Exam exam = await unitOfWork.Exams.GetById(id);
            var orgId = exam.OrganizationId;
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);

            if (user.IsExaminerOfOrg(orgId) || user.IsAdminOfOrg(orgId))
            {
                exam = Mapper.Map(examDto, exam);
                unitOfWork.Exams.Update(exam);
                await unitOfWork.SaveAsync();
                return Ok(exam);
            }
            else
            {
                return Unauthorized("you are not Authorized");
            }
        }


        //// POST: api/Exams
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ExamDto>> PostExam([FromBody]Exam exam)
        {
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (user.IsExaminerOfOrg(exam.OrganizationId))
            {
                await unitOfWork.Exams.Add(exam);
                int result = await unitOfWork.SaveAsync();

                if (result > 0)
                {
                    return Ok(Mapper.Map<ExamDto>(exam));
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Unauthorized("you are not an Examiner");
            }

        }

        // DELETE: api/Exams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Exam>> DeleteExam(int id)
        {
            var exam = await unitOfWork.Exams.SingleOrDefault(e => e.Id == id);
            if (exam == null) return BadRequest();
            var orgId = exam.OrganizationId;
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (user == null) return Unauthorized();
            if (!(user.IsObserverOfOrg(orgId) || user.IsExaminerOfOrg(orgId) || user.IsAdminOfOrg(orgId))) return Forbid();
            unitOfWork.Exams.Remove(exam);
            return Ok(await unitOfWork.SaveAsync());
        }

        // PUT: api/Exams/questions
        [HttpPut("questions")]
        [Authorize]
        public async Task<IActionResult> PutExamQuestions(ExamQuestionsDto examQuestionsDto)
        {
            Exam exam = await unitOfWork.Exams.GetById(examQuestionsDto.ExamId);
            if (exam == null) return BadRequest();
            var orgId = exam.OrganizationId;
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (!(user.IsOwnerOfOrg(orgId) || user.IsExaminerOfOrg(orgId) || user.IsAdminOfOrg(orgId))) return Forbid();
            unitOfWork.ExamQuestions.RemoveRange(await unitOfWork.ExamQuestions.Find(eq => eq.ExamId == exam.Id));
            unitOfWork.ExamComplexQuestions.RemoveRange(await unitOfWork.ExamComplexQuestions.Find(ecq => ecq.ExamId == exam.Id));
            await unitOfWork.SaveAsync();
            List<ExamQuestion> questions = new List<ExamQuestion>();
            List<ExamComplexQuestion> complexQuestions = new List<ExamComplexQuestion>();
            foreach (int quesId in examQuestionsDto.QuestionsIds)
            {
                Question q = await unitOfWork.Questions.GetById(quesId);
                if (q == null) return BadRequest();
                if (q.IsPublic || q.OrganizationId == exam.OrganizationId)
                questions.Add(new ExamQuestion { ExamId = exam.Id, QuestionId = quesId });   
            };
            foreach (int comQuesId in examQuestionsDto.ComplexQuestionsIds)
            {
                ComplexQuestion cq = await unitOfWork.ComplexQuestions.GetById(comQuesId);
                if (cq == null) return BadRequest();
                if (cq.IsPublic || cq.OrganizationId == exam.OrganizationId)
                complexQuestions.Add(new ExamComplexQuestion { ExamId = exam.Id, ComplexQuestionId = comQuesId });
            };
            await unitOfWork.ExamQuestions.AddRange(questions);
            await unitOfWork.ExamComplexQuestions.AddRange(complexQuestions);
            await unitOfWork.SaveAsync();
            return Ok(Mapper.Map<ExamDto>(await unitOfWork.Exams.GetExamWithQuestions(examQuestionsDto.ExamId)));
        }
    }
}
