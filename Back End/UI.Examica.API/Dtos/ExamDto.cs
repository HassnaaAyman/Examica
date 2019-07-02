using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Examica.API.Dtos
{
    public class ExamDto
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Mark { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public List<QuestionDto> Questions { get; set; }
        public List<ComplexQuestionDto> ComplexQuestions { get; set; }
    }
}
