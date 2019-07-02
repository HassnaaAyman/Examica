using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Persistence.EntityConfigurations
{
    public class ExamConfig : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.Property(exam => exam.Name).IsRequired();
            builder.Property(exam => exam.IsPublic).HasDefaultValue(false);
            builder.HasOne(exam => exam.Organization)
                .WithMany(org => org.Exams)
                .HasForeignKey(exam => exam.OrganizationId);
        }
    }
}
