using JuniorHub.Application.Contracts.Services;
using JuniorHub.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace JuniorHub.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add services
        services.AddScoped<ITechnologyService, TechnologyService>();
        services.AddScoped<IEmployerService, EmployerService>();
        services.AddScoped<IFreelancerService, FreelancerService>();
        services.AddScoped<IOfferService, OfferService>();
        services.AddScoped<IFreelancerValorationService, FreelancerValorationService>();
        services.AddScoped<IEmployerValorationService, EmployerValorationService>();
        services.AddScoped<IOfferApplicationService, OfferApplicationService>();

        // FluentValidation configuration
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
}
