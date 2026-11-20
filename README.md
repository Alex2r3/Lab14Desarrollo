# Lab14Desarrollo

Proyecto .NET con integración continua y despliegue automatizado usando GitHub Actions.

## Estructura del Proyecto
- **Lab13-2-AlexanderVasquez.Api**: API Web ASP.NET Core con Swagger
- **Lab13-2-AlexanderVasquez.Application**: Lógica de aplicación, casos de uso y MediatR
- **Lab13-2-AlexanderVasquez.Domain**: Entidades y reglas de negocio del dominio
- **Lab13-2-AlexanderVasquez.Infrastructure**: Persistencia con Entity Framework Core (PostgreSQL)

## Pipeline de CI/CD (GitHub Actions)
El workflow configurado en `.github/workflows/dotnet.yml` realiza:
1. Checkout del código fuente
2. Configuración del entorno .NET SDK
3. Restauración de paquetes (`dotnet restore`)
4. Compilación del proyecto en configuración Release (`dotnet build`)
5. Publicación de la aplicación en la carpeta `./published` (`dotnet publish`)
6. Ejecución del paso de despliegue simulado hacia el servidor destino
