using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Examica.API.Dtos
{
    public class ComplexQuestionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int OrganizationId { get; set; }
        public bool IsPublic { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}
