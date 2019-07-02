using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UI.Examica.Model.Core.Domains.Enums;

namespace UI.Examica.Model.Core.Domains
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DifficultyLevel Level { get; set; }
        public QuestionType Type { get; set; }
        public int Mark { get; set; }
        public bool IsPublic { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public List<QuestionOption> QuestionOptions { get; set; }
        public List<ExamQuestion> ExamQuestions { get; set; }
        public List<Answer> Answers { get; set; }
        public List<QuestionComplexQuestion> QuestionComplexQuestions { get; set; }
    }
}
