using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Examica.Model.Core.Domains
{
    public class PricingPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int MaxNoOfAdmins { get; set; }
        public int MaxNoOfExaminers { get; set; }
        public int MaxNoOfExaminees { get; set; }
        public int MaxNoOfObservers { get; set; }
        public List<Organization> Organizations { get; set; }
    }
}
