using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using UI.Examica.Model.Core.Domains;
using UI.Examica.Model.Core.Repository;

namespace UI.Examica.Model.Persistence.Repository
{
    public class AppUserRepo : Repository<AppUser>, IAppUserRepo
    {
        public AppUserRepo(DbContext _context) : base(_context)
        {
        }
        public AppUser GetUserWithOrgs(string id)
        {
            return  entities.Include(user => user.Organizations)
                            .Include(user => user.OrganizationAdmins)
                            .ThenInclude(oa => oa.Organization)
                            .Include(user => user.OrganizationExaminees)
                            .ThenInclude(oee => oee.Organization)
                            .Include(user => user.OrganizationExaminers)
                            .ThenInclude(oer => oer.Organization)
                            .Include(user => user.OrganizationObservers)
                            .ThenInclude(oo => oo.Organization)
                            .FirstOrDefault(user => user.Id == id);
        }
    }
}
