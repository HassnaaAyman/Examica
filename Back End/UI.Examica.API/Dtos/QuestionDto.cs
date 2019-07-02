using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UI.Examica.Model.Core.Domains.Enums;

namespace UI.Examica.API.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int OrganizationId { get; set; }
        public string Level { get; set; }
        public string Type { get; set; }
        public int Mark { get; set; }
        public IEnumerable<OptionDto> Options { get; set; }
        public bool IsPublic { get; set; }
    }
}
