using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Examica.Model.Core.Domains
{
    public class Option
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuestionOption> QuestionOptions { get; set; }
    }
}
