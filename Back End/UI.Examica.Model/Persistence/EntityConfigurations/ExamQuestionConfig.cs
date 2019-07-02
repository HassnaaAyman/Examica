using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Persistence.EntityConfigurations
{
    public class ExamQuestionConfig : IEntityTypeConfiguration<ExamQuestion>
    {
        public void Configure(EntityTypeBuilder<ExamQuestion> builder)
        {
            builder.HasKey(examQ => new { examQ.QuestionId, examQ.ExamId });
            builder.HasOne(examQ => examQ.Question)
                .WithMany(question => question.ExamQuestions)
                .HasForeignKey(examQ => examQ.QuestionId);
            builder.HasOne(examQ => examQ.Exam)
               .WithMany(exam => exam.ExamQuestions)
               .HasForeignKey(examQ => examQ.ExamId);
        }
    }
}
