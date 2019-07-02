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
    public class ExamRepo : Repository<Exam>, IExamRepo
    {
        public ExamRepo(DbContext _context) : base(_context)
        {
        }

        public async Task<Exam> GetExamWithQuestions(int id)
        {
            //List<Exam> exams = await entities.ToListAsync();
            //for (int i = 0; i < 10; i++)
            //{
            //    exams.ElementAtOrDefault(new Random().Next() % exams.Count());
            //}
            return await entities.Include(exam => exam.ExamQuestions) 
                                   .ThenInclude(eq => eq.Question)
                                   .ThenInclude(q => q.QuestionOptions)
                                   .ThenInclude(qo => qo.Option)
                                   .Include(exam => exam.ExamComplexQuestions)
                                   .ThenInclude(ecq => ecq.ComplexQuestion)
                                   .ThenInclude(cq => cq.QuestionComplexQuestions)
                                   .ThenInclude(qcq => qcq.Question)
                                   .ThenInclude(cq => cq.QuestionOptions)
                                   .ThenInclude(qo => qo.Option)
                                    .FirstOrDefaultAsync(e=>e.Id== id);
        }
    }
}
