using JunioHub.Application.Contracts.Services;
using JunioHub.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace JunioHub.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add services
        services.AddScoped<ITechnologyService, TechnologyService>();
        services.AddScoped<IEmployerService, EmployerService>();
        services.AddScoped<IFreelancerService, FreelancerService>();
        services.AddScoped<IValorationService, ValorationService>();

        // FluentValidation configuration
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
}
