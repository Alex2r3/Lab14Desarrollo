using Lab13_2_AlexanderVasquez.Api.Configuration;
using Lab13_2_AlexanderVasquez.Infrastructure.Configuration;
using Lab13_2_AlexanderVasquez.Infrastructure.Data;
using Lab13_2_AlexanderVasquez.Domain.Models;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

// Mostrar detalles de errores para facilitar depuración en Render
app.UseDeveloperExceptionPage();

// Habilitar Swagger en la ruta /swagger y redirigir la raíz / hacia /swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab13-2 API v1");
    c.RoutePrefix = "swagger";
});

app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Inicializar tablas y datos iniciales si la base de datos está disponible
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.EnsureCreated();

        if (!db.Clients.Any())
        {
            db.Clients.AddRange(
                new Client { Name = "Alexander Vasquez", Email = "alexander.vasquez@gmail.com" },
                new Client { Name = "Carlos Mendoza", Email = "carlos.mendoza@gmail.com" }
            );
            db.SaveChanges();
        }

        if (!db.Products.Any())
        {
            db.Products.AddRange(
                new Product { Name = "Laptop Gamer", Description = "Core i7 16GB SSD 512GB", Price = 1299.99m },
                new Product { Name = "Teclado Mecanico", Description = "RGB Switch Red", Price = 79.50m },
                new Product { Name = "Mouse Inalambrico", Description = "Optico 16000 DPI", Price = 49.90m }
            );
            db.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[AVISO] No se pudo inicializar la base de datos en inicio: {ex.Message}");
    }
}

app.Run();
