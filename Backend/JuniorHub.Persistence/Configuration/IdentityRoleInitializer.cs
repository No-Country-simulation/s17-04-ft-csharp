using JuniorHub.Domain.Enums;
using JuniorHub.Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorHub.Persistence.Configuration
{
    internal class IdentityRoleInitializer : IEntityTypeConfiguration<IdentityRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
        {
            builder.HasData(
           new IdentityRole<int> { Id = 1, Name = Role.Admin.ToStringEnum(), NormalizedName = Role.Admin.ToStringEnum().ToUpper() },
           new IdentityRole<int> { Id = 2, Name = Role.Freelancer.ToStringEnum(), NormalizedName = Role.Freelancer.ToStringEnum().ToUpper() },
           new IdentityRole<int> { Id = 3, Name = Role.Employer.ToStringEnum(), NormalizedName = Role.Employer.ToStringEnum().ToUpper() }
             );
        }
    }
}
