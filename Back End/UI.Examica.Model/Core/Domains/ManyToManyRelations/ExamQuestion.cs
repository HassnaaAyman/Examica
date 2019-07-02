using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Examica.Model.Core.Domains
{
    public class ExamQuestion
    {
         public int QuestionId { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public Question Question { get; set; }
    }
}
