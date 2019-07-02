using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Examica.API.Dtos
{
    public class AuthDto
    {
        public string UserId { get; set; }
        public int OrgId { get; set; }
        public string Token { get; set; }
    }
}
