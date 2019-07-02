using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Core.Domains
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string OwnerId { get; set; }
        public int PricingPlanId { get; set; }
        public AppUser Owner { get; set; }
        public PricingPlan PricingPlan { get; set; }
        public List<OrganizationAdmin> OrganizationAdmins { get; set; }
        public List<OrganizationExaminee> OrganizationExaminees { get; set; }
        public List<OrganizationExaminer> OrganizationExaminers { get; set; }
        public List<OrganizationObserver> OrganizationObservers { get; set; }
        public List<Question> Questions { get; set; }
        public List<ComplexQuestion> ComplexQuestions { get; set; }
        public List<Exam> Exams { get; set; }

    }
}
