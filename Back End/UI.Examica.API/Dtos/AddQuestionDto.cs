using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UI.Examica.Model.Core.Domains;
using UI.Examica.Model.Core.Domains.Enums;

namespace UI.Examica.API.Dtos
{
    public class AddQuestionDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public DifficultyLevel Level { get; set; }
        [Required]
        public QuestionType Type { get; set; }
        [Required]
        public int Mark { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int OrganizationId { get; set; }
        public bool IsPublic { get; set; }
        public IEnumerable<Option> Options { get; set; }
    }
}
