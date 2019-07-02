using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UI.Examica.Model.Core.Repository;

namespace UI.Examica.Model.Core
{
    public interface IUnitOfWork
    {
        IAnswerRepo Answers { get; }
        IAppUserRepo AppUsers { get; }
        IComplexQuestionRepo ComplexQuestions { get; }
        IExamAppUserRepo ExamAppUsers { get; }
        IExamComplexQuestionRepo ExamComplexQuestions { get; }
        IExamQuestionRepo ExamQuestions { get; }
        IExamRepo Exams { get; }
        IOptionRepo Options { get; }
        IPricingPlanRepo PricingPlans { get; }
        IOrganizationAdminRepo OrganizationAdmins { get; }
        IOrganizationExamineeRepo organizationExaminees { get; }
        IOrganizationExaminerRepo OrganizationExaminers { get; }
        IOrganizationObserverRepo OrganizationObservers { get; }
        IOrganizationRepo Organizations { get; }
        IQuestionComplexQuestionRepo QuestionComplexQuestions { get; }
        IQuestionOptionRepo QuestionOptions { get; }
        IQuestionRepo Questions { get; }
        IResultRepo Results { get; }
        int Save();
        Task<int> SaveAsync();
    }
}
