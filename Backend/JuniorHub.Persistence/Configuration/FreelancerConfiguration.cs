using JuniorHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorHub.Persistence.Configuration;

internal class FreelancerConfiguration : IEntityTypeConfiguration<Freelancer>
{
    public void Configure(EntityTypeBuilder<Freelancer> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).UseIdentityColumn();

        builder.HasOne(f => f.User)
            .WithOne()
            .HasForeignKey<Freelancer>(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.Links)
            .WithOne(l => l.Freelancer)
            .HasForeignKey(l => l.FreelancerId);

        builder.HasMany(f => f.Applications)
            .WithOne(a => a.Freelancer)
            .HasForeignKey(a => a.FreelancerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(f => f.FreelancerValorations)
               .WithOne(fv => fv.Freelancer)
               .HasForeignKey(fv => fv.FreelancerId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(f => f.EmployerValorations)
               .WithOne(ev => ev.Freelancer)
               .HasForeignKey(ev => ev.FreelancerId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
