using JuniorHub.Domain.Entities;
using JuniorHub.Domain.Enums;
using JuniorHub.Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JuniorHub.Persistence.Seeding;

public static class Seed
{
    public static void IntialSeed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityRole<int>>().HasData(
            new IdentityRole<int> { Id = 1, Name = Role.Admin.ToStringEnum(), NormalizedName = Role.Admin.ToStringEnum().ToUpper() },
            new IdentityRole<int> { Id = 2, Name = Role.Freelancer.ToStringEnum(), NormalizedName = Role.Freelancer.ToStringEnum().ToUpper() },
            new IdentityRole<int> { Id = 3, Name = Role.Employer.ToStringEnum(), NormalizedName = Role.Employer.ToStringEnum().ToUpper() }
        );

        //Programming Languages
        modelBuilder.Entity<Technology>().HasData(
            new Technology { Id = 1, Name = "C#" },
            new Technology { Id = 2, Name = "JavaScript" },
            new Technology { Id = 3, Name = "Python" },
            new Technology { Id = 4, Name = "Java" },
            new Technology { Id = 5, Name = "Ruby" },
            new Technology { Id = 6, Name = "PHP" },
            new Technology { Id = 7, Name = "Swift" },
            new Technology { Id = 8, Name = "Kotlin" },
            new Technology { Id = 9, Name = "Go" },
            new Technology { Id = 10, Name = "TypeScript" }
        );
        //Frameworks
        modelBuilder.Entity<Technology>().HasData(
            new Technology { Id = 11, Name = "React" },
            new Technology { Id = 12, Name = "Angular" },
            new Technology { Id = 13, Name = "Vue.js" },
            new Technology { Id = 14, Name = "ASP.NET Core" },
            new Technology { Id = 15, Name = "Django" },
            new Technology { Id = 16, Name = "Flask" },
            new Technology { Id = 17, Name = "Ruby on Rails" },
            new Technology { Id = 18, Name = "Spring Boot" },
            new Technology { Id = 19, Name = "Laravel" },
            new Technology { Id = 20, Name = "Express.js" },
            new Technology { Id = 21, Name = "Svelte" }
        );

        //DB:
        modelBuilder.Entity<Technology>().HasData(
            new Technology { Id = 22, Name = "MySQL" },
            new Technology { Id = 23, Name = "PostgreSQL" },
            new Technology { Id = 24, Name = "MongoDB" },
            new Technology { Id = 25, Name = "SQLite" },
            new Technology { Id = 26, Name = "Oracle" },
            new Technology { Id = 27, Name = "Microsoft SQL Server" },
            new Technology { Id = 28, Name = "Firebase" }
        );

        //Backend:
        modelBuilder.Entity<Technology>().HasData(
            new Technology { Id = 29, Name = "Node.js" },
            new Technology { Id = 30, Name = ".NET" },
            new Technology { Id = 31, Name = "Ruby" },
            new Technology { Id = 32, Name = "PHP" },
            new Technology { Id = 33, Name = "Java EE" },
            new Technology { Id = 34, Name = "Flask" }
        );
    }
}