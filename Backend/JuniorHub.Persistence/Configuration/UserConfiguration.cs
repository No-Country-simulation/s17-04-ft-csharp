using JuniorHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorHub.Persistence.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name).HasMaxLength(50);
            builder.Property(u => u.LastName).HasMaxLength(50);
            
        }
    }

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
                .HasForeignKey(o=>o.IdFreelancer);

            builder.HasMany(f => f.Valorations)
                .WithOne(v => v.Freelancer)
                .HasForeignKey(v => v.IdFreelancer);

            
        }
    }

    internal class EmployerConfiguration : IEntityTypeConfiguration<Employer>
    {


        public void Configure(EntityTypeBuilder<Employer> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).UseIdentityColumn();

            builder.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employer>(f => f.IdUser);

            builder.HasMany(e => e.Offers)
                .WithOne(o => o.Employer)
                .HasForeignKey(o => o.IdEmployer);

            builder.HasMany(e => e.Valorations)
                .WithOne(v => v.Employer)
                .HasForeignKey(v => v.IdEmployer);

        }
    }

    internal class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).UseIdentityColumn();

            builder.HasMany(e => e.Applications)
                .WithOne(o => o.Offer)
                .HasForeignKey(o => o.IdOffer);

        }
    }

    internal class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.HasKey(of => new { of.IdOffer, of.IdFreelancer });
        }
    }

    internal class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
    {
        public void Configure(EntityTypeBuilder<Technology> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).UseIdentityColumn();

            builder.HasMany(f => f.Freelancers)
                .WithMany(s => s.Technologies);
        }
    }

    internal class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).UseIdentityColumn();

            builder.HasMany(f => f.Freelancers)
                .WithMany(s => s.Skills);
        }
    }

    internal class LinkConfiguration : IEntityTypeConfiguration<Link>
    {
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).UseIdentityColumn();
        }
    }

    internal class ValorationConfiguration : IEntityTypeConfiguration<Valoration>
    {
        public void Configure(EntityTypeBuilder<Valoration> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(v=>v.Id).UseIdentityColumn();
        }
    }
}
