using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Examica.Model.Core.Domains
{
    public class QuestionComplexQuestion
    {
        public int QuestionId { get; set; }
        public int ComplexQuestionId { get; set; }
        public Question Question { get; set; }
        public ComplexQuestion ComplexQuestion { get; set; }
    }
}
