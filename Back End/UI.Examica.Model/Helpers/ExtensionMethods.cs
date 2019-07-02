using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Helpers
{
    public static class ExtensionMethods
    {
        public static bool IsOwnerOfOrg(this AppUser user, int orgId)
        {
            Organization organization = user.Organizations.SingleOrDefault(org => org.Id == orgId);
            return organization != null;
        }
        public static bool IsAdminOfOrg(this AppUser user, int orgId)
        {
            OrganizationAdmin organization = user.OrganizationAdmins.SingleOrDefault(oa => oa.OrgnaizationId == orgId);
            return organization != null;
        }
        public static bool IsExaminerOfOrg(this AppUser user, int orgId)
        {
            OrganizationExaminer organization = user.OrganizationExaminers.SingleOrDefault(org => org.OrgnaizationId == orgId);
            return organization != null;
        }
        public static bool IsExamineeOfOrg(this AppUser user, int orgId)
        {
            OrganizationExaminee organization = user.OrganizationExaminees.SingleOrDefault(org => org.OrgnaizationId == orgId);
            return organization != null;
        }
        public static bool IsObserverOfOrg(this AppUser user, int orgId)
        {
            OrganizationObserver organization = user.OrganizationObservers.SingleOrDefault(org => org.OrgnaizationId == orgId);
            return organization != null;
        }
    }
}
