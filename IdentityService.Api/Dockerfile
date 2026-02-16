# =========================
# BUILD STAGE
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["IdentityService.Api/IdentityService.Api.csproj", "IdentityService.Api/"]
COPY ["AppJob.Core/AppJob.Core.csproj", "AppJob.Core/"]
COPY ["IdentityService.Application/IdentityService.Application.csproj", "IdentityService.Application/"]
COPY ["IdentityService.Domain/IdentityService.Domain.csproj", "IdentityService.Domain/"]
COPY ["IdentityService.Persistence/IdentityService.Persistence.csproj", "IdentityService.Persistence/"]
COPY ["IdentityService.Infrastructure/IdentityService.Infrastructure.csproj", "IdentityService.Infrastructure/"]
COPY ["JobSeeker.Shared.Common/JobSeeker.Shared.Common.csproj", "JobSeeker.Shared.Common/"]
COPY ["JobSeeker.Shared.Kernel/JobSeeker.Shared.Kernel.csproj", "JobSeeker.Shared.Kernel/"]
COPY ["JobSeeker.Shared.Contracts/JobSeeker.Shared.Contracts.csproj", "JobSeeker.Shared.Contracts/"]
COPY ["JobSeeker.Shared.Models/JobSeeker.Shared.Models.csproj", "JobSeeker.Shared.Models/"]
COPY ["JobSeeker.Shared.EventBusRabbitMQ/JobSeeker.Shared.EventBusRabbitMQ.csproj", "JobSeeker.Shared.EventBusRabbitMQ/"]
COPY ["JobSeeker.Shared.Logging/JobSeeker.Shared.Logging.csproj", "JobSeeker.Shared.Logging/"]
COPY ["JobSeeker.Shared.PushNotifications/JobSeeker.Shared.PushNotifications.csproj", "JobSeeker.Shared.PushNotifications/"]

# Restore dependencies
RUN dotnet restore "IdentityService.Api/IdentityService.Api.csproj"

# Copy everything else
COPY . .

# Build the project
WORKDIR "/src/IdentityService.Api"
RUN dotnet build "IdentityService.Api.csproj" -c Release -o /app/build

# =========================
# PUBLISH STAGE
# =========================
FROM build AS publish
RUN dotnet publish "IdentityService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# =========================
# RUNTIME STAGE
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published files
COPY --from=publish /app/publish .

# Expose container port
EXPOSE 80

# Healthcheck for Docker
HEALTHCHECK --interval=30s --timeout=3s --start-period=10s --retries=3 \
  CMD curl --fail http://localhost/health || exit 1

# Metadata
LABEL version="1.0" maintainer="behzadeskandari"

# Entry point
ENTRYPOINT ["dotnet", "IdentityService.Api.dll"]


## See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
## This stage is used when running from VS in fast mode (Default for Debug configuration)
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER $APP_UID
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#
## This stage is used to build the service project
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["IdentityService.Api/IdentityService.Api.csproj", "IdentityService.Api/"]
#COPY ["AppJob.Core/AppJob.Core.csproj", "AppJob.Core/"]
#COPY ["IdentityService.Application/IdentityService.Application.csproj", "IdentityService.Application/"]
#COPY ["IdentityService.Domain/IdentityService.Domain.csproj", "IdentityService.Domain/"]
#COPY ["JobSeeker.Shared.Contracts/JobSeeker.Shared.Contracts.csproj", "JobSeeker.Shared.Contracts/"]
#COPY ["JobSeeker.Shared.Kernel/JobSeeker.Shared.Kernel.csproj", "JobSeeker.Shared.Kernel/"]
#COPY ["JobSeeker.Shared.Models/JobSeeker.Shared.Models.csproj", "JobSeeker.Shared.Models/"]
#COPY ["IdentityService.Infrastructure/IdentityService.Infrastructure.csproj", "IdentityService.Infrastructure/"]
#COPY ["JobSeeker.Shared.Common/JobSeeker.Shared.Common.csproj", "JobSeeker.Shared.Common/"]
#COPY ["ProfileService.Infrastructure/ProfileService.Infrastructure.csproj", "ProfileService.Infrastructure/"]
#COPY ["ProfileService.Domain/ProfileService.Domain.csproj", "ProfileService.Domain/"]
#COPY ["JobSeeker.Shared.EventBusRabbitMQ/JobSeeker.Shared.EventBusRabbitMQ.csproj", "JobSeeker.Shared.EventBusRabbitMQ/"]
#COPY ["JobSeeker.Shared.Logging/JobSeeker.Shared.Logging.csproj", "JobSeeker.Shared.Logging/"]
#COPY ["IdentityService.Persistence/IdentityService.Persistence.csproj", "IdentityService.Persistence/"]
#COPY ["JobSeeker.Shared.PushNotifications/JobSeeker.Shared.PushNotifications.csproj", "JobSeeker.Shared.PushNotifications/"]
#RUN dotnet restore "./IdentityService.Api/IdentityService.Api.csproj"
#COPY . .
#WORKDIR "/src/IdentityService.Api"
#RUN dotnet build "./IdentityService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
## This stage is used to publish the service project to be copied to the final stage
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./IdentityService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "IdentityService.Api.dll"]