using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;
using UI.Examica.Model.Persistence.EntityConfigurations;

namespace UI.Examica.Model.Persistence
{
    public class ExamicaDbContext : IdentityDbContext<AppUser>
    {
        public ExamicaDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<ComplexQuestion> ComplexQuestions { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<ExamAppUser> ExamAppUsers { get; set; }
        public virtual DbSet<ExamComplexQuestion> ExamComplexQuestions { get; set; }
        public virtual DbSet<ExamQuestion> ExamQuestions { get; set; }
        public virtual DbSet<OrganizationAdmin> OrganizationAdmins { get; set; }
        public virtual DbSet<OrganizationExaminee> OrganizationExaminees { get; set; }
        public virtual DbSet<OrganizationExaminer> OrganizationExaminers { get; set; }
        public virtual DbSet<OrganizationObserver> OrganizationObservers { get; set; }
        public virtual DbSet<QuestionComplexQuestion> QuestionComplexQuestions { get; set; }
        public virtual DbSet<QuestionOption> QuestionOptions { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<PricingPlan> PricingPlans { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new ComplexQuestionConfig());
            builder.ApplyConfiguration(new ExamConfig());
            builder.ApplyConfiguration(new OptionConfig());
            builder.ApplyConfiguration(new OrganizationConfig());
            builder.ApplyConfiguration(new QuestionConfig());
            builder.ApplyConfiguration(new AnswerConfig());
            builder.ApplyConfiguration(new ExamAppUserConfig());
            builder.ApplyConfiguration(new ExamComplexQuestionConfig());
            builder.ApplyConfiguration(new ExamQuestionConfig());
            builder.ApplyConfiguration(new OrganizationAdminConfig());
            builder.ApplyConfiguration(new OrganizationExamineeConfig());
            builder.ApplyConfiguration(new OrganizationExaminerConfig());
            builder.ApplyConfiguration(new OrganizationObserverConfig());
            builder.ApplyConfiguration(new QuestionComplexQuestionConfig());
            builder.ApplyConfiguration(new QuestionOptionConfig());
            builder.ApplyConfiguration(new ResultConfig());
            builder.ApplyConfiguration(new PricingPlanConfig());
        }
    }
}
