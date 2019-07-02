using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Persistence.EntityConfigurations
{
    public class PricingPlanConfig : IEntityTypeConfiguration<PricingPlan>
    {
        public void Configure(EntityTypeBuilder<PricingPlan> builder)
        {
            builder.HasIndex(p => p.Name).IsUnique();
        }
    }
}
