using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Examica.Model.Core.Domains
{
    public class ExamComplexQuestion
    {
         public int ComplexQuestionId { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public ComplexQuestion ComplexQuestion { get; set; }
    }
}
