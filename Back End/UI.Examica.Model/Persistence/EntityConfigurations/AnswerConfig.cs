using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Persistence.EntityConfigurations
{
    public class AnswerConfig : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(answer => new { answer.ExamId, answer.QuestionId, answer.AppUserId });
            builder.HasOne(answer => answer.Exam)
                .WithMany(exam => exam.Answers)
                .HasForeignKey(answer => answer.ExamId);
            builder.HasOne(answer => answer.Question)
                .WithMany(question => question.Answers)
                .HasForeignKey(answer => answer.QuestionId);
            builder.HasOne(answer => answer.AppUser)
                .WithMany(user => user.Answers)
                .HasForeignKey(answer => answer.AppUserId);
            builder.Property(answer => answer.IsCorrect)
                .HasDefaultValue(false);
        }
    }
}
