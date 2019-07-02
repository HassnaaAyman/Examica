using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UI.Examica.Model.Core.Domains;
using UI.Examica.Model.Core.Repository;

namespace UI.Examica.Model.Persistence.Repository
{
    public class PricingPlanRepo : Repository<PricingPlan>, IPricingPlanRepo
    {
        public PricingPlanRepo(DbContext _context) : base(_context)
        {
        }
    }
}
