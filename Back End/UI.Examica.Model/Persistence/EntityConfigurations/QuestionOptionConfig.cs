using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Persistence.EntityConfigurations
{
    public class QuestionOptionConfig : IEntityTypeConfiguration<QuestionOption>
    {
        public void Configure(EntityTypeBuilder<QuestionOption> builder)
        {
            builder.HasKey(questionOpt => new { questionOpt.OptionId, questionOpt.QuestionId });
            builder.HasOne(questionOpt => questionOpt.Option)
                .WithMany(option => option.QuestionOptions)
                .HasForeignKey(questionOpt => questionOpt.OptionId);
            builder.HasOne(questionOpt => questionOpt.Question)
                .WithMany(ques => ques.QuestionOptions)
                .HasForeignKey(questionOpt => questionOpt.QuestionId);
        }
    }
}
