using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Helpers
{
    public class UserComparer : IEqualityComparer<AppUser>
    {
        public bool Equals(AppUser x, AppUser y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(AppUser obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
