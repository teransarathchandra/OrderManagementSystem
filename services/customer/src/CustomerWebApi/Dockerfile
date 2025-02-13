# Use the official .NET image as a base runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install required package for SQL Server connectivity
#RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*

# Use the .NET SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy directory.build.props first to ensure build settings are available
COPY Directory.Build.props .

# Copy .csproj files for all projects
COPY customer/src/CustomerWebApi/CustomerWebApi.csproj CustomerWebApi/
COPY customer/src/Application/Application.csproj Application/
COPY customer/src/Infrastructure/Infrastructure.csproj Infrastructure/
COPY customer/src/Domain/Domain.csproj Domain/
COPY shared/Shared.csproj ../shared/

# Restore dependencies
RUN dotnet restore "CustomerWebApi/CustomerWebApi.csproj"

# Copy the entire solution
COPY customer/src/ /src/
COPY shared/ ../shared/

# Build the application
WORKDIR /src/CustomerWebApi
RUN dotnet build "CustomerWebApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "CustomerWebApi.csproj" -c Release -o /app/publish

# Use the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerWebApi.dll"]
