using JuniorHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Persistence.Data
{
    internal class JuniorHubContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public JuniorHubContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employer> Employers { get; set; }
        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Valoration> Valorations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
