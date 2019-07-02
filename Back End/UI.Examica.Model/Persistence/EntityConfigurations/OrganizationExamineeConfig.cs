using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.Model.Persistence.EntityConfigurations
{
    public class OrganizationExamineeConfig : IEntityTypeConfiguration<OrganizationExaminee>
    {
        public void Configure(EntityTypeBuilder<OrganizationExaminee> builder)
        {
            builder.HasKey(orgExaminee => new { orgExaminee.AppUserId, orgExaminee.OrgnaizationId });
            builder.HasOne(orgExaminee => orgExaminee.Organization)
                .WithMany(org => org.OrganizationExaminees)
                .HasForeignKey(orgExaminee => orgExaminee.OrgnaizationId);
            builder.HasOne(orgExaminee => orgExaminee.AppUser)
              .WithMany(user => user.OrganizationExaminees)
              .HasForeignKey(orgExaminee => orgExaminee.AppUserId);
        }
    }
}
