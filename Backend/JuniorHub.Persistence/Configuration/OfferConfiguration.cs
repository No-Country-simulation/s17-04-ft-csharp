using JuniorHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorHub.Persistence.Configuration
{
    internal class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).UseIdentityColumn();

            builder.HasMany(e => e.Applications)
                .WithOne(o => o.Offer)
                .HasForeignKey(o => o.IdOffer)
                .OnDelete(DeleteBehavior.Restrict); 

        }
    }
}
