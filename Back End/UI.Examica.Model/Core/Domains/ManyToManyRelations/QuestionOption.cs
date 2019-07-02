using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Examica.Model.Core.Domains
{
    public class QuestionOption
    {
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
        public Question Question { get; set; }
        public Option Option { get; set; }

    }
}
