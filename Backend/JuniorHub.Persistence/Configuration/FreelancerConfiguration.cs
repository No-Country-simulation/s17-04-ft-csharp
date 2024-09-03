using JuniorHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorHub.Persistence.Configuration
{
    internal class FreelancerConfiguration : IEntityTypeConfiguration<Freelancer>
    {
        public void Configure(EntityTypeBuilder<Freelancer> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).UseIdentityColumn();

            builder.HasOne(f => f.User)
                .WithOne()
                .HasForeignKey<Freelancer>(f => f.IdUser);

            builder.HasMany(f => f.Links)
                .WithOne(l => l.Freelancer)
                .HasForeignKey(l=>l.FreelancerId);

            builder.HasMany(f => f.Skills)
                .WithMany(s => s.Freelancers);

            builder.HasMany(f => f.Technologies)
               .WithMany(s => s.Freelancers);

            builder.HasMany(f => f.Applications)
                .WithOne(o=>o.Freelancer)
                .HasForeignKey(o=>o.IdFreelancer)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(f => f.Valorations)
                .WithOne(v => v.Freelancer)
                .HasForeignKey(v => v.IdFreelancer)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
