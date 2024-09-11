using JuniorHub.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JuniorHub.Persistence.Configuration;

public class EmployerValorationConfiguration : IEntityTypeConfiguration<EmployerValoration>
{
    public void Configure(EntityTypeBuilder<EmployerValoration> builder)
    {
        builder.ToTable("EmployerValorations");

        builder.HasKey(ev => ev.Id);

        builder.HasOne(ev => ev.Employer)
            .WithMany(e => e.EmployerValorations)
            .HasForeignKey(ev => ev.EmployerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ev => ev.Freelancer)
            .WithMany(f => f.EmployerValorations)
            .HasForeignKey(ev => ev.FreelancerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(ev => ev.ValorationValue)
            .IsRequired();

        builder.Property(ev => ev.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
