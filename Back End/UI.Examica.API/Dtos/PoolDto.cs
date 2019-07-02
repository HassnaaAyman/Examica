using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Examica.API.Dtos
{
    public class PoolDto
    {
        public List<QuestionDto> Questions { get; set; }
        public List<ComplexQuestionDto> ComplexQuestions { get; set; }
    }
}
