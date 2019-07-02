using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using UI.Examica.Model.Core.Domains;
using UI.Examica.Model.Core.Repository;

namespace UI.Examica.Model.Persistence.Repository
{
    public class AnswerRepo : Repository<Answer>, IAnswerRepo
    {
        public AnswerRepo(DbContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<Answer>> GetByExamId()
        {
            return await entities.Include(a => a.Exam).ToListAsync();
        }

        public async Task<IEnumerable<Answer>> GetByExamIdAndUser()
        {
            return await entities.Include(a => a.AppUser)
                   .Include(a => a.Exam)
                   .ToListAsync();
        }

        public async Task<IEnumerable<Answer>> GetByExamIdWithMembers()
        {
            return await entities.Include(a => a.Question)
                    .Include(a => a.AppUser)
                    .Include(a => a.Exam)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Answer>> GetByQuestionAndUser()
        {
            return await entities.Include(a => a.Question)
                   .Include(a => a.AppUser)
                   .ToListAsync();
        }
    }
}
