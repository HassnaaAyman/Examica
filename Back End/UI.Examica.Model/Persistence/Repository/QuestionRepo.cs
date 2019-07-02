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
    public class QuestionRepo : Repository<Question>, IQuestionRepo
    {
        public QuestionRepo(DbContext _context) : base(_context)
        {
        }
        public async Task<IEnumerable<Question>> GetQuestionsWithOptions()
        {
            return await entities.Include(q => q.QuestionOptions)
                        .ThenInclude(qo => qo.Option).ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsOfOrg(int orgId)
        {
            return await entities.Include(q => q.QuestionOptions)
                           .ThenInclude(qo => qo.Option).Where(q => q.OrganizationId == orgId || q.IsPublic)
                           .ToListAsync();
        }

        public async Task<Question> GetQuestionWithOptionsById(int quesId)
        {
            return await entities.Include(q => q.QuestionOptions)
                       .ThenInclude(qo => qo.Option).FirstOrDefaultAsync(q => q.Id == quesId);
        }
    }
}
