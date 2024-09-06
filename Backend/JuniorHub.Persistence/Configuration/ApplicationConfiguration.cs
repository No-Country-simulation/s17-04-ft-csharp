using JuniorHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorHub.Persistence.Configuration;

internal class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne(a => a.Offer)
               .WithMany(o => o.Applications)
               .HasForeignKey(a => a.OfferId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.Freelancer)
               .WithMany(f => f.Applications)
               .HasForeignKey(a => a.FreelancerId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
