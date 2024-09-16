using JuniorHub.Application.Contracts.Cloudinary;
using JuniorHub.Application.Contracts.SendGrid;
using JuniorHub.SendGrid.Helpers;
using JuniorHub.SendGrid.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JuniorHub.SendGrid;
public static class SendGridServiceExtensions
{
    public static IServiceCollection AddSendGridServiceExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        var sendGridConfiguration = new SendGridConfiguration();
        configuration.Bind("SendGridConfiguration", sendGridConfiguration);

        services.AddSingleton(sendGridConfiguration);
        services.AddScoped<IEmailService, SendGridEmailService>();

        return services;
    }
}
