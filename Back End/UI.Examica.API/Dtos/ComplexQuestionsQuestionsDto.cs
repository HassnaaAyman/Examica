using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Examica.API.Dtos
{
    public class ComplexQuestionsQuestionsDto
    {
        public int ComplexQuestionId { get; set; }
        public List<int> QuestionsIds { get; set; }
    }
}
