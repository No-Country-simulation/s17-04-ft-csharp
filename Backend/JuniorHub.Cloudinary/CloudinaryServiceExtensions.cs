using JuniorHub.Application.Contracts.Cloudinary;
using JuniorHub.Cloudinary.Helpers;
using JuniorHub.Cloudinary.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JuniorHub.Cloudinary;

public static class CloudinaryServiceExtensions
{
    public static IServiceCollection AddCloudServiceExtensions(this IServiceCollection services,IConfiguration configuration)
    {
        var cloudConfiguration = new CloudinaryConfiguration();
        configuration.Bind("CloudinaryConfiguration", cloudConfiguration);

        services.AddSingleton(cloudConfiguration);
        services.AddScoped<ICloudinaryService, CloudinaryService>();

        return services;
    }
}
