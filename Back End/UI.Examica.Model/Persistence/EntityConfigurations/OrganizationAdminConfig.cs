using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Persistence.EntityConfigurations
{
    public class OrganizationAdminConfig : IEntityTypeConfiguration<OrganizationAdmin>
    {
        public void Configure(EntityTypeBuilder<OrganizationAdmin> builder)
        {
            builder.HasKey(orgAdmin => new { orgAdmin.AppUserId, orgAdmin.OrgnaizationId });
            builder.HasOne(orgAdmin => orgAdmin.Organization)
                .WithMany(org => org.OrganizationAdmins)
                .HasForeignKey(orgAdmin => orgAdmin.OrgnaizationId);
            builder.HasOne(orgAdmin => orgAdmin.AppUser)
              .WithMany(user => user.OrganizationAdmins)
              .HasForeignKey(orgAdmin => orgAdmin.AppUserId);
        }
    }
}
