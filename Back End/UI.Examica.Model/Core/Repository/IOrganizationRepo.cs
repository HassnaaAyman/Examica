using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Core.Repository
{
    public interface IOrganizationRepo : IRepository<Organization>
    {
        Task<bool> IsExistedAsync(Expression<Func<Organization, bool>> predicate);
        Task<Organization> GetOrganizationWithUsers(int orgId);
    }
}
