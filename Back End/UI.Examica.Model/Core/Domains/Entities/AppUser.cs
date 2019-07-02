using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Examica.Model.Core.Domains
{
    public class AppUser : IdentityUser
    {
        public List<Answer> Answers { get; set; }
        public List<Result> Results { get; set; }
        public List<ExamAppUser> ExamAppUsers { get; set; }
        public List<Organization> Organizations { get; set; }
        public List<OrganizationAdmin> OrganizationAdmins { get; set; }
        public List<OrganizationExaminee> OrganizationExaminees { get; set; }
        public List<OrganizationExaminer> OrganizationExaminers { get; set; }
        public List<OrganizationObserver> OrganizationObservers { get; set; }
    }
}
