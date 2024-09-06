using JuniorHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorHub.Persistence.Configuration;

internal class LinkConfiguration : IEntityTypeConfiguration<Link>
{
    public void Configure(EntityTypeBuilder<Link> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id).UseIdentityColumn();

        builder.HasOne(l => l.Freelancer)
           .WithMany(f => f.Links)
           .HasForeignKey(l => l.FreelancerId);
    }
}
