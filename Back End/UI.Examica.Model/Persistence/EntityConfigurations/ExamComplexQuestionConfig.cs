using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Persistence.EntityConfigurations
{
    public class ExamComplexQuestionConfig : IEntityTypeConfiguration<ExamComplexQuestion>
    {
        public void Configure(EntityTypeBuilder<ExamComplexQuestion> builder)
        {
            builder.HasKey(examComp => new { examComp.ComplexQuestionId, examComp.ExamId });
            builder.HasOne(examComp => examComp.Exam)
                .WithMany(exam => exam.ExamComplexQuestions)
                .HasForeignKey(examComp => examComp.ExamId);
            builder.HasKey(examComp => new { examComp.ComplexQuestionId, examComp.ExamId });
            builder.HasOne(examComp => examComp.ComplexQuestion)
                .WithMany(comp => comp.examComplexQuestions)
                .HasForeignKey(examComp => examComp.ComplexQuestionId);
        }
    }
}
