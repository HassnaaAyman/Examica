using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Examica.API.Dtos;

namespace UI.Examica.API.Helpers
{
    public class OrganizationDtoComparer : IEqualityComparer<OrganizationDto>
    {
        public bool Equals(OrganizationDto x, OrganizationDto y)
        {
            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode(OrganizationDto obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
