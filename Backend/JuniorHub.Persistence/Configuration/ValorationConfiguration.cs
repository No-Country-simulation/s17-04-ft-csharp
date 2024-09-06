using JuniorHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorHub.Persistence.Configuration;

internal class ValorationConfiguration : IEntityTypeConfiguration<Valoration>
{
    public void Configure(EntityTypeBuilder<Valoration> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v=>v.Id).UseIdentityColumn();

        builder.HasOne(v => v.Freelancer)
            .WithMany(f => f.Valorations)
            .HasForeignKey(v => v.FreelancerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(v => v.Employer)
            .WithMany(e => e.Valorations)
            .HasForeignKey(v => v.EmployerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
