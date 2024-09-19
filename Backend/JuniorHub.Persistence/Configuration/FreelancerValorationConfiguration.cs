using JuniorHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorHub.Persistence.Configuration;

public class FreelancerValorationConfiguration : IEntityTypeConfiguration<FreelancerValoration>
{
    public void Configure(EntityTypeBuilder<FreelancerValoration> builder)
    {
        builder.ToTable("FreelancerValorations");

        builder.HasKey(fv => fv.Id);

        builder.HasOne(fv => fv.Freelancer)
            .WithMany(f => f.FreelancerValorations)
            .HasForeignKey(fv => fv.FreelancerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(fv => fv.Employer)
            .WithMany(e => e.FreelancerValorations)
            .HasForeignKey(fv => fv.EmployerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(fv => fv.ValorationValue)
            .IsRequired();

        builder.Property(fv => fv.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
