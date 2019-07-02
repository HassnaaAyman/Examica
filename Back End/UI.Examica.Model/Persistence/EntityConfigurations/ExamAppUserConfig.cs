using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Persistence.EntityConfigurations
{
    public class ExamAppUserConfig : IEntityTypeConfiguration<ExamAppUser>
    {
        public void Configure(EntityTypeBuilder<ExamAppUser> builder)
        {
            builder.HasKey(examApp => new { examApp.ExamId, examApp.AppUserId });
            builder.HasOne(examApp => examApp.AppUser)
                .WithMany(user => user.ExamAppUsers)
                .HasForeignKey(examApp => examApp.AppUserId);
            builder.HasOne(examApp => examApp.Exam)
                .WithMany(exam=> exam.ExamAppUsers)
                .HasForeignKey(examApp => examApp.ExamId);
        }
    }
}
