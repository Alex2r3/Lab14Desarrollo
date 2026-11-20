using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lab13_2_AlexanderVasquez.Application.Ports;
using Lab13_2_AlexanderVasquez.Domain.Repositories;
using Lab13_2_AlexanderVasquez.Infrastructure.Adapters.Repositories;
using Lab13_2_AlexanderVasquez.Infrastructure.Services;
using Lab13_2_AlexanderVasquez.Infrastructure.Data;

namespace Lab13_2_AlexanderVasquez.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
                               ?? "Host=localhost;Database=LINQExample;Username=postgres;Password=Clave321;Port=5432";

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderdetailRepository, OrderdetailRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IExcelReportService, ExcelReportService>();

        return services;
    }
}
