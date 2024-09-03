using JuniorHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorHub.Persistence.Configuration
{
    internal class EmployerConfiguration : IEntityTypeConfiguration<Employer>
    {


        public void Configure(EntityTypeBuilder<Employer> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).UseIdentityColumn();

            builder.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employer>(f => f.UserId);

            builder.HasMany(e => e.Offers)
                .WithOne(o => o.Employer)
                .HasForeignKey(o => o.EmployerId);

            builder.HasMany(e => e.Valorations)
                .WithOne(v => v.Employer)
                .HasForeignKey(v => v.EmployerId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
