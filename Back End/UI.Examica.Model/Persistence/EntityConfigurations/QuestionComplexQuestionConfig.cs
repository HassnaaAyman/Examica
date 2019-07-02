using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Persistence.EntityConfigurations
{
    public class QuestionComplexQuestionConfig : IEntityTypeConfiguration<QuestionComplexQuestion>
    {
        public void Configure(EntityTypeBuilder<QuestionComplexQuestion> builder)
        {
            builder.HasKey(questionComp => new { questionComp.ComplexQuestionId, questionComp.QuestionId });
            builder.HasOne(questionComp => questionComp.ComplexQuestion)
                .WithMany(comp => comp.QuestionComplexQuestions)
                .HasForeignKey(questionComp => questionComp.ComplexQuestionId);
            builder.HasOne(questionComp => questionComp.Question)
                .WithMany(ques => ques.QuestionComplexQuestions)
                .HasForeignKey(questionComp => questionComp.QuestionId);
        }
    }
}
