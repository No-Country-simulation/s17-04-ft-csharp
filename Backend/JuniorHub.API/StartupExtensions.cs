using JunioHub.Application;
using JuniorHub.Persistence;
using JuniorHub.Mapping;
using Microsoft.OpenApi.Models;
using System.Reflection;
using JuniorHub.API.Middleware;
using JuniorHub.Mapping.Profiles;
using JuniorHub.Cloudinary;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JuniorHub.API;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();
        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddCloudServiceExtensions(builder.Configuration);
        builder.Services.AddMappingProfiles();
        //builder.Services.AddAutoMapper(typeof(EmployerProfile));
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddSwagger();

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                   .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();

            });
        });
        builder.Services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "JuniorHub API");
            });
        }

        app.UseHttpsRedirection();
        //app.UseStaticFiles();
        app.UseRouting();

        app.UseCustomExceptionHandler();

        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }

    private static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "JuniorHub API",

            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },new string[]{ }
                    }
                });

            //Configuración para añadir comentarios en XML
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath,includeControllerXmlComments:true);

            c.OperationFilter<SwaggerAuthorizeCheckOperationFilter>();
        });

        
    }

    public class SwaggerAuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authorizeAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                var roles = authorizeAttributes
                    .Where(attr => !string.IsNullOrEmpty(attr.Roles))
                    .Select(attr => attr.Roles)
                    .Distinct();

                var rolesText = roles.Any() ? $"Roles: {string.Join(", ", roles)}" : "Authorization required";

                operation.Description += $"<br/><b>{rolesText}</b>";
            }
        }
    }

}