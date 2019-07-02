using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UI.Examica.Model.Core;
using UI.Examica.Model.Core.Repository;
using UI.Examica.Model.Persistence.Repository;

namespace UI.Examica.Model.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExamicaDbContext context;
        public UnitOfWork(ExamicaDbContext _context)
        {
            context = _context;
            Answers = new AnswerRepo(_context);
            AppUsers = new AppUserRepo(_context);
            ComplexQuestions = new ComplexQuestionRepo(_context);
            ExamAppUsers = new ExamAppUserRepo(_context);
            ExamComplexQuestions = new ExamComplexQuestionRepo(_context);
            ExamQuestions = new ExamQuestionRepo(_context);
            Exams = new ExamRepo(_context);
            Options = new OptionRepo(_context);
            OrganizationAdmins = new OrganizationAdminRepo(_context);
            organizationExaminees = new OrganizationExamineeRepo(_context);
            OrganizationExaminers = new OrganizationExaminerRepo(_context);
            OrganizationObservers = new OrganizationObsereverRepo(_context);
            Organizations = new OrganizationRepo(_context);
            QuestionComplexQuestions = new QuestionComplexQuestionRepo(_context);
            Questions = new QuestionRepo(_context);
            QuestionOptions = new QuestionOptionRepo(_context);
            Results = new ResultRepo(_context);
            PricingPlans = new PricingPlanRepo(_context);
        }
        public IAnswerRepo Answers { get; }

        public IAppUserRepo AppUsers { get; }

        public IComplexQuestionRepo ComplexQuestions { get; }

        public IExamAppUserRepo ExamAppUsers { get; }

        public IExamComplexQuestionRepo ExamComplexQuestions { get; }

        public IExamQuestionRepo ExamQuestions { get; }

        public IExamRepo Exams { get; }

        public IOptionRepo Options { get; }

        public IOrganizationAdminRepo OrganizationAdmins { get; }

        public IOrganizationExamineeRepo organizationExaminees { get; }

        public IOrganizationExaminerRepo OrganizationExaminers { get; }

        public IOrganizationObserverRepo OrganizationObservers { get; }

        public IOrganizationRepo Organizations { get; }

        public IQuestionComplexQuestionRepo QuestionComplexQuestions { get; }

        public IQuestionOptionRepo QuestionOptions { get; }

        public IQuestionRepo Questions { get; }

        public IResultRepo Results { get; }

        public IPricingPlanRepo PricingPlans { get; }

        public int Save()
        {
            return context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
