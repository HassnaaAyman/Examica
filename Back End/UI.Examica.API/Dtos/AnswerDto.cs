using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Examica.API.Dtos
{
    public class AnswerDto
    {
        public string UserAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public int ExamId { get; set; }
        public int QuestionId { get; set; }
        public string AppUserId { get; set; }
    }
}
