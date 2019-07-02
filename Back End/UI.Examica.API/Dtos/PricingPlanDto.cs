using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UI.Examica.API.Dtos
{
   public  class PricingPlanDto
    {
        [Required]
        public int  Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Price { get; set; }
        public int MaxNoOfAdmins { get; set; }
        public int MaxNoOfExaminers { get; set; }
        public int MaxNoOfExaminees { get; set; }
        public int MaxNoOfObservers { get; set; }

    }
}
