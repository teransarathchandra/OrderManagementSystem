# Use the official .NET image as a base runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install required package for SQL Server connectivity
RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*

# Use the .NET SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy .csproj files for all projects
COPY services/catalog/src/CatalogWebApi/CatalogWebApi.csproj CatalogWebApi/
COPY services/catalog/src/Application/Application.csproj Application/
COPY services/catalog/src/Infrastructure/Infrastructure.csproj Infrastructure/
COPY services/catalog/src/Domain/Domain.csproj Domain/

# Restore dependencies
RUN dotnet restore "CatalogWebApi/CatalogWebApi.csproj"

# Copy the entire solution
COPY services/catalog/src/ /src/

# Build the application
WORKDIR /src/CatalogWebApi
RUN dotnet build "CatalogWebApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "CatalogWebApi.csproj" -c Release -o /app/publish

# Use the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogWebApi.dll"]