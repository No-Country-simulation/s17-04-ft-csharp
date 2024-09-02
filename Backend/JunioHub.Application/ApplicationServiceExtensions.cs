using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JunioHub.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add services

        // FluentValidation configuration

        return services;
    }
}
