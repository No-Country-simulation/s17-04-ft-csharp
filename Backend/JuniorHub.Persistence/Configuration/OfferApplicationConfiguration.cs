using JuniorHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorHub.Persistence.Configuration;

internal class OfferApplicationConfiguration : IEntityTypeConfiguration<OfferApplication>
{
    public void Configure(EntityTypeBuilder<OfferApplication> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).UseIdentityColumn();

        builder.HasOne(a => a.Offer)
               .WithMany(o => o.Applications)
               .HasForeignKey(a => a.OfferId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.Freelancer)
               .WithMany(f => f.Applications)
               .HasForeignKey(a => a.FreelancerId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.Property(a => a.ApplicationDate)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
