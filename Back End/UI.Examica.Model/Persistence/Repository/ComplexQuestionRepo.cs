using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UI.Examica.Model.Core.Domains;
using UI.Examica.Model.Core.Repository;

namespace UI.Examica.Model.Persistence.Repository
{
    public class ComplexQuestionRepo : Repository<ComplexQuestion>, IComplexQuestionRepo
    {
        public ComplexQuestionRepo(DbContext _context) : base(_context)
        {
        }

        public async Task<List<ComplexQuestion>> GetComplexsWithSubByOrgId(int orgId)
        {
            return await entities
                        .Include(cq => cq.QuestionComplexQuestions)
                        .ThenInclude(qcq => qcq.Question)
                        .ThenInclude(q => q.QuestionOptions)
                        .ThenInclude(qo => qo.Option)
                        .Where(cq => cq.OrganizationId == orgId || cq.IsPublic).ToListAsync();
        }

        public async Task<ComplexQuestion> GetComplexWithSubById(int id)
        {
            return await entities
                        .Include(cq => cq.QuestionComplexQuestions)
                        .ThenInclude(qcq => qcq.Question)
                        .ThenInclude(q => q.QuestionOptions)
                        .ThenInclude(qo => qo.Option)
                        .FirstOrDefaultAsync(cq => cq.Id == id);
        }
    }
}
