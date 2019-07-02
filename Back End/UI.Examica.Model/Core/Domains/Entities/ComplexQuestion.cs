using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Examica.Model.Core.Domains
{
    public class ComplexQuestion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int OrganizationId { get; set; }
        public bool IsPublic { get; set; }
        public Organization Organization { get; set; }
        public List<QuestionComplexQuestion> QuestionComplexQuestions { get; set; }
        public List<ExamComplexQuestion> examComplexQuestions { get; set; }
    }
}
