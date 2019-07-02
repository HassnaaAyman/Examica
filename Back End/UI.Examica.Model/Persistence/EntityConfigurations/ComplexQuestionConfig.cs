using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Persistence.EntityConfigurations
{
    public class ComplexQuestionConfig : IEntityTypeConfiguration<ComplexQuestion>
    {
        public void Configure(EntityTypeBuilder<ComplexQuestion> builder)
        {
            builder.Property(compQuestion => compQuestion.IsPublic).HasDefaultValue(false);
            builder.HasOne(complex => complex.Organization)
                .WithMany(organization => organization.ComplexQuestions)
                .HasForeignKey(complex => complex.OrganizationId);
        }
    }
}
