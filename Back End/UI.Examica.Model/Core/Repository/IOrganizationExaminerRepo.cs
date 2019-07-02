using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Core.Repository
{
    public interface IOrganizationExaminerRepo : IRepository<OrganizationExaminer>
    {
    }
}
