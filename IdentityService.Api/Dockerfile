
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore as distinct layers - EXACT CASING MATTERS IN DOCKER!
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
COPY ["ProfileService.Infrastructure/ProfileService.Infrastructure.csproj", "ProfileService.Infrastructure/"]
COPY ["ProfileService.Domain/ProfileService.Domain.csproj", "ProfileService.Domain/"]

RUN dotnet restore "IdentityService.Api/IdentityService.Api.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/IdentityService.Api"
RUN dotnet build "IdentityService.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "IdentityService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
#EXPOSE 80
##HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 CMD curl --fail http://localhost/health || exit 1
LABEL version="1.0" maintainer="behzadeskandari"
ENTRYPOINT ["dotnet", "IdentityService.Api.dll"]

#
## this stage is used when running from vs in fast mode (default for debug configuration)
#from mcr.microsoft.com/dotnet/aspnet:8.0 as base
#user $app_uid
#workdir /app
#expose 8080
#expose 8081
#
#
## this stage is used to build the service project
#from mcr.microsoft.com/dotnet/sdk:8.0 as build
#arg build_configuration=release
#workdir /src
#copy ["identityservice.api/identityservice.api.csproj", "identityservice.api/"]
#copy ["appjob.core/appjob.core.csproj", "appjob.core/"]
#copy ["identityservice.application/identityservice.application.csproj", "identityservice.application/"]
#copy ["identityservice.domain/identityservice.domain.csproj", "identityservice.domain/"]
#copy ["jobseeker.shared.contracts/jobseeker.shared.contracts.csproj", "jobseeker.shared.contracts/"]
#copy ["jobseeker.shared.kernel/jobseeker.shared.kernel.csproj", "jobseeker.shared.kernel/"]
#copy ["jobseeker.shared.models/jobseeker.shared.models.csproj", "jobseeker.shared.models/"]
#copy ["identityservice.infrastructure/identityservice.infrastructure.csproj", "identityservice.infrastructure/"]
#copy ["jobseeker.shared.common/jobseeker.shared.common.csproj", "jobseeker.shared.common/"]
#copy ["profileservice.infrastructure/profileservice.infrastructure.csproj", "profileservice.infrastructure/"]
#copy ["profileservice.domain/profileservice.domain.csproj", "profileservice.domain/"]
#copy ["jobseeker.shared.eventbusrabbitmq/jobseeker.shared.eventbusrabbitmq.csproj", "jobseeker.shared.eventbusrabbitmq/"]
#copy ["jobseeker.shared.logging/jobseeker.shared.logging.csproj", "jobseeker.shared.logging/"]
#copy ["identityservice.persistence/identityservice.persistence.csproj", "identityservice.persistence/"]
#copy ["jobseeker.shared.pushnotifications/jobseeker.shared.pushnotifications.csproj", "jobseeker.shared.pushnotifications/"]
#run dotnet restore "./identityservice.api/identityservice.api.csproj"
#copy . .
#workdir "/src/identityservice.api"
#run dotnet build "./identityservice.api.csproj" -c $build_configuration -o /app/build
#
## this stage is used to publish the service project to be copied to the final stage
#from build as publish
#arg build_configuration=release
#run dotnet publish "./identityservice.api.csproj" -c $build_configuration -o /app/publish /p:useapphost=false
#
## this stage is used in production or when running from vs in regular mode (default when not using the debug configuration)
#from base as final
#workdir /app
#copy --from=publish /app/publish .
#entrypoint ["dotnet", "identityservice.api.dll"]