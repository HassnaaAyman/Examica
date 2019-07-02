using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Examica.API.Dtos;

namespace UI.Examica.API.Helpers
{
    public class UserDtoComparer : IEqualityComparer<UserDto>
    {
        public bool Equals(UserDto x, UserDto y)
        {
            return x.Id == y.Id && x.Email == y.Email;
        }

        public int GetHashCode(UserDto obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
