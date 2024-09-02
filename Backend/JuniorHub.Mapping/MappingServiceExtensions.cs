using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JuniorHub.Mapping;

public static class MappingServiceExtensions
{
    public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}