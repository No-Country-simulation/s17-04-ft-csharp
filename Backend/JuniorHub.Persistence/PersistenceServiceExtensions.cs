using JunioHub.Application.Contracts.Persistence;
using JuniorHub.Domain.Entities;
using JuniorHub.Domain.Enums;
using JuniorHub.Domain.Helpers;
using JuniorHub.Domain.Utilities;
using JuniorHub.Persistence.Data;
using JuniorHub.Persistence.Identity;
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

        var jwtConfig = new JwtConfiguration();
        configuration.Bind("JwtConfiguration", jwtConfig);
        services.AddSingleton(jwtConfig);
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<UserManager<User>>();
        services.AddScoped<SignInManager<User>>();
        services.AddScoped<RoleManager<IdentityRole<int>>>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Role.Employer.ToStringEnum(), policy =>
                policy.RequireRole(Role.Employer.ToStringEnum()));

            options.AddPolicy(Role.Freelancer.ToStringEnum(), policy =>
                policy.RequireRole(Role.Freelancer.ToStringEnum()));

            options.AddPolicy(Role.Admin.ToStringEnum(), policy =>
                policy.RequireRole(Role.Admin.ToStringEnum()));
        });
        return services;
    }
}
