using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Persistence.EntityConfigurations
{
    public class OrganizationObserverConfig : IEntityTypeConfiguration<OrganizationObserver>
    {
        public void Configure(EntityTypeBuilder<OrganizationObserver> builder)
        {
            builder.HasKey(orgObserver => new { orgObserver.AppUserId, orgObserver.OrgnaizationId });
            builder.HasOne(orgObserver => orgObserver.Organization)
                .WithMany(org => org.OrganizationObservers)
                .HasForeignKey(orgObserver => orgObserver.OrgnaizationId);
            builder.HasOne(orgObserver => orgObserver.AppUser)
              .WithMany(user => user.OrganizationObservers)
              .HasForeignKey(orgObserver => orgObserver.AppUserId);
        }
    }
}
