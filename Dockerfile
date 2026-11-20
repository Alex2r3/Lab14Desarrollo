# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and csproj files for layer caching
COPY *.sln ./
COPY Lab13-2-AlexanderVasquez.Domain/*.csproj ./Lab13-2-AlexanderVasquez.Domain/
COPY Lab13-2-AlexanderVasquez.Application/*.csproj ./Lab13-2-AlexanderVasquez.Application/
COPY Lab13-2-AlexanderVasquez.Infrastructure/*.csproj ./Lab13-2-AlexanderVasquez.Infrastructure/
COPY Lab13-2-AlexanderVasquez.Api/*.csproj ./Lab13-2-AlexanderVasquez.Api/

RUN dotnet restore

# Copy all source files and publish Release build
COPY . ./
WORKDIR /src/Lab13-2-AlexanderVasquez.Api
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

ENV ASPNETCORE_HTTP_PORTS=8080
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Lab13-2-AlexanderVasquez.Api.dll"]
