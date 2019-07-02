using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Persistence.EntityConfigurations
{
    public class OrganizationConfig : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.Property(org => org.Name).IsRequired();
            builder.HasOne(org => org.Owner)
                .WithMany(user => user.Organizations)
                .HasForeignKey(org => org.OwnerId);
            builder.HasOne(org => org.PricingPlan)
                .WithMany(p => p.Organizations)
                .HasForeignKey(org => org.PricingPlanId);
        }
    }
}
