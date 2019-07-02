using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Examica.API.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
