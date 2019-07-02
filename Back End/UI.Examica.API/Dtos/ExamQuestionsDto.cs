using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Examica.API.Dtos
{
    public class ExamQuestionsDto
    {
        public int ExamId { get; set; }
        public List<int> QuestionsIds { get; set; }
        public List<int> ComplexQuestionsIds { get; set; }
    }
}
