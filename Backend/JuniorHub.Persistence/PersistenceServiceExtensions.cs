
using JuniorHub.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using JuniorHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Identity.Core;

namespace JuniorHub.Persistence;

public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<JuniorHubContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("JuniorHubConnectionString"));
        });
        //services.AddIdentity<User, IdentityRole<int>>()
        //   .AddEntityFrameworkStores<JuniorHubContext>().AddDefaultTokenProviders();


        return services;
    }
}
