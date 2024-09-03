using JuniorHub.Domain.Entities;
using JuniorHub.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JuniorHub.Persistence;

public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<JuniorHubContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("JuniorHubConnectionString"));
        });
        services.AddIdentity<User, IdentityRole<int>>()
           .AddEntityFrameworkStores<JuniorHubContext>().AddDefaultTokenProviders();


        return services;
    }
}
