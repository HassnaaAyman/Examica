using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Core.Domains
{
    public class Exam
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrganizationId { get; set; }
        public int Mark { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool IsPublic { get; set; }
        public Organization Organization { get; set; }
        public List<Answer> Answers { get; set; }
        public List<Result> Results { get; set; }
        public List<ExamAppUser> ExamAppUsers { get; set; }
        public List<ExamQuestion> ExamQuestions { get; set; }
        public List<ExamComplexQuestion> ExamComplexQuestions { get; set; }
    }
}
