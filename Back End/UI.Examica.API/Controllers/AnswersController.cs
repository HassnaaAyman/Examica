using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI.Examica.API.Dtos;
using UI.Examica.Model.Core;
using UI.Examica.Model.Core.Domains;
using UI.Examica.Model.Persistence;

namespace UI.Examica.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public AnswersController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        // GET: api/Answers
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
        {
            //await unitOfWork.Answers.GetByExamIdWithMembers();
            return Ok(await unitOfWork.Answers.GetAll());
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswersByExamId(int id)
        {
            await unitOfWork.Answers.GetByExamId();
            var answers = await unitOfWork.Answers.Find(ans => ans.ExamId == id);

            if (answers == null)
            {
                return NotFound();
            }

            return Ok(answers);
        }

        [HttpGet("[action]/{examId}/{userId}/{flag}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswersByExamIdUserIdAndIfCorrect(int examId, string userId, bool flag)
        {
            await unitOfWork.Answers.GetByExamIdAndUser();
            var answers = await unitOfWork.Answers
                .Find(ans => ans.ExamId == examId && ans.IsCorrect == flag && ans.AppUserId == userId);

            if (answers == null)
            {
                return NotFound();
            }

            return Ok(answers);
        }

        [HttpGet("[action]/{userId}/{examId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswersByExamIdAndUserId(string userId, int examId)
        {
            await unitOfWork.Answers.GetByExamIdAndUser();
            var answers = await unitOfWork.Answers
                .Find(ans => ans.ExamId == examId && ans.AppUserId == userId);

            if (answers == null)
            {
                return NotFound();
            }

            return Ok(answers);
        }

        [HttpGet("[action]/{userId}/{quesId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswersByQuestionIdAndUserId(string userId, int quesId)
        {
            await unitOfWork.Answers.GetByQuestionAndUser();
            var answers = await unitOfWork.Answers
                .Find(ans => ans.QuestionId == quesId && ans.AppUserId == userId);

            if (answers == null)
            {
                return NotFound();
            }

            return Ok(answers);
        }

        //Gets one answer
        [HttpGet("[action]/{userId}/{examId}/{quesId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Answer>> GetAnswer(string userId, int examId, int quesId)
        {
            await unitOfWork.Answers.GetByExamIdWithMembers();
            var _answer = await unitOfWork.Answers
                .SingleOrDefault(e => e.QuestionId == quesId && e.ExamId == examId && e.AppUserId == userId);
            if (_answer == null)
            {
                return NotFound();
            }

            return Ok(_answer);
        }

        // PUT: api/Answers/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAnswerByExamId(Answer answer)
        {
            Answer ans = await unitOfWork.Answers.GetById(answer.ExamId, answer.QuestionId, answer.AppUserId);
            if (ans != null)
            {
                unitOfWork.Answers.Update(answer);
                int x = await unitOfWork.SaveAsync();
                if (x > 0)
                {
                    return Ok(answer);
                }
                else
                {
                    return BadRequest();
                }
            }
            return NotFound();

        }

        // POST: api/Answers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Answer>> PostAnswer(Answer answer)
        {
            Answer ans = await unitOfWork.Answers.GetById(answer.ExamId, answer.QuestionId, answer.AppUserId);
            if (ans != null) return BadRequest();
            Exam exam = await unitOfWork.Exams.GetById(answer.ExamId);
            Question question = await unitOfWork.Questions.GetById(answer.QuestionId);
            AppUser user = await unitOfWork.AppUsers.GetById(answer.AppUserId);
            if (exam == null || question == null || user == null) return BadRequest();
            await unitOfWork.Answers.Add(answer);
            int x = await unitOfWork.SaveAsync();
            if (x > 0)
            {
                return Ok(new AnswerDto{
                    UserAnswer = answer.UserAnswer,
                    AppUserId = answer.AppUserId, QuestionId = answer.QuestionId,
                    ExamId =answer.ExamId, IsCorrect=answer.IsCorrect });
            }
            else
            {
            return BadRequest();
            }
        }

    }
}
