using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Persistence.EntityConfigurations
{
    public class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(question => question.Title).IsRequired();
            builder.Property(question => question.Level).IsRequired();
            builder.Property(question => question.Type).IsRequired();
            builder.Property(question => question.Mark).IsRequired();
            builder.Property(question => question.IsPublic).HasDefaultValue(false);
            builder.HasOne(question => question.Organization)
                .WithMany(org => org.Questions)
                .HasForeignKey(question => question.OrganizationId);
        }
    }
}
