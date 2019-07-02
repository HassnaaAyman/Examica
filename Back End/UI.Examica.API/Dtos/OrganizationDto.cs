using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Examica.API.Dtos
{
    public class OrganizationDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required]
        public string OwnerId { get; set; }
        public int PricingPlanId { get; set; }
    }
}
