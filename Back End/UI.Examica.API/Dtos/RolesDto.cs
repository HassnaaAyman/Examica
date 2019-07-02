using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Examica.API.Dtos
{
    public class RolesDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int OrganizationId { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        [Required]
        public bool IsExaminer { get; set; }
        [Required]
        public bool IsExaminee { get; set; }
        [Required]
        public bool IsObserver { get; set; }
        public bool IsOwner { get; set; }
    }
}
