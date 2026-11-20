using Microsoft.OpenApi.Models;
using Lab13_2_AlexanderVasquez.Application;

namespace Lab13_2_AlexanderVasquez.Api.Configuration;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(ApplicationAssemblyMarker).Assembly
        ));

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Lab 13-2 API (Lab 08 con Arquitectura Hexagonal y CQRS)",
                Version = "v1",
                Description = "API REST con Arquitectura Hexagonal, CQRS con MediatR, Patrón Repositorio y Generación de Reportes Excel con ClosedXML."
            });
        });

        return services;
    }
}
