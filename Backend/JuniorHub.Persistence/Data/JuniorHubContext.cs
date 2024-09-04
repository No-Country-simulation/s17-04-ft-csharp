using JuniorHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JuniorHub.Persistence.Data;

public class JuniorHubContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public JuniorHubContext(DbContextOptions<JuniorHubContext> options) : base(options)
    {
    }

    public DbSet<Employer> Employers { get; set; }
    public DbSet<Freelancer> Freelancers { get; set; }
    public DbSet<Link> Links { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<Technology> Technologies { get; set; }
    public DbSet<Valoration> Valorations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        Seeding.Seed.IntialSeed(builder);
    }
}
