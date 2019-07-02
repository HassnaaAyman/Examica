using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Persistence.EntityConfigurations
{
    public class OrganizationExaminerConfig : IEntityTypeConfiguration<OrganizationExaminer>
    {
        public void Configure(EntityTypeBuilder<OrganizationExaminer> builder)
        {
            builder.HasKey(orgExaminer => new { orgExaminer.AppUserId, orgExaminer.OrgnaizationId });
            builder.HasOne(orgExaminer => orgExaminer.Organization)
                .WithMany(org => org.OrganizationExaminers)
                .HasForeignKey(orgExaminer => orgExaminer.OrgnaizationId);
            builder.HasOne(orgExaminer => orgExaminer.AppUser)
              .WithMany(user => user.OrganizationExaminers)
              .HasForeignKey(orgExaminer => orgExaminer.AppUserId);
        }
    }
}
