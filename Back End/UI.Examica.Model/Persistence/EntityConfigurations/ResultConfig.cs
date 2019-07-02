using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Persistence.EntityConfigurations
{
    public class ResultConfig : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.HasKey(result => new { result.ExamId, result.UserId });
            builder.HasOne(result => result.Exam)
                .WithMany(exam => exam.Results)
                .HasForeignKey(result => result.ExamId);
            builder.HasOne(result => result.User)
               .WithMany(user => user.Results)
               .HasForeignKey(result => result.UserId);
        }
    }
}
