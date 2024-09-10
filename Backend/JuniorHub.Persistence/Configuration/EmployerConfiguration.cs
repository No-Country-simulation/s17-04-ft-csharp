using JuniorHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorHub.Persistence.Configuration;

internal class EmployerConfiguration : IEntityTypeConfiguration<Employer>
{
    public void Configure(EntityTypeBuilder<Employer> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityColumn();

        builder.HasOne(e => e.User)
            .WithOne()
            .HasForeignKey<Employer>(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Offers)
            .WithOne(o => o.Employer)
            .HasForeignKey(o => o.EmployerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.FreelancerValorations)
               .WithOne(fv => fv.Employer)
               .HasForeignKey(fv => fv.EmployerId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.EmployerValorations)
               .WithOne(ev => ev.Employer)
               .HasForeignKey(ev => ev.EmployerId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
