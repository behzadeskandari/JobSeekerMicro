
# this stage is used when running from vs in fast mode (default for debug configuration)
from mcr.microsoft.com/dotnet/aspnet:8.0 as base
user $app_uid
workdir /app
expose 8080
expose 8081


# this stage is used to build the service project
from mcr.microsoft.com/dotnet/sdk:8.0 as build
arg build_configuration=release
workdir /src
copy ["identityservice.api/identityservice.api.csproj", "identityservice.api/"]
copy ["appjob.core/appjob.core.csproj", "appjob.core/"]
copy ["identityservice.application/identityservice.application.csproj", "identityservice.application/"]
copy ["identityservice.domain/identityservice.domain.csproj", "identityservice.domain/"]
copy ["jobseeker.shared.contracts/jobseeker.shared.contracts.csproj", "jobseeker.shared.contracts/"]
copy ["jobseeker.shared.kernel/jobseeker.shared.kernel.csproj", "jobseeker.shared.kernel/"]
copy ["jobseeker.shared.models/jobseeker.shared.models.csproj", "jobseeker.shared.models/"]
copy ["identityservice.infrastructure/identityservice.infrastructure.csproj", "identityservice.infrastructure/"]
copy ["jobseeker.shared.common/jobseeker.shared.common.csproj", "jobseeker.shared.common/"]
copy ["profileservice.infrastructure/profileservice.infrastructure.csproj", "profileservice.infrastructure/"]
copy ["profileservice.domain/profileservice.domain.csproj", "profileservice.domain/"]
copy ["jobseeker.shared.eventbusrabbitmq/jobseeker.shared.eventbusrabbitmq.csproj", "jobseeker.shared.eventbusrabbitmq/"]
copy ["jobseeker.shared.logging/jobseeker.shared.logging.csproj", "jobseeker.shared.logging/"]
copy ["identityservice.persistence/identityservice.persistence.csproj", "identityservice.persistence/"]
copy ["jobseeker.shared.pushnotifications/jobseeker.shared.pushnotifications.csproj", "jobseeker.shared.pushnotifications/"]
run dotnet restore "./identityservice.api/identityservice.api.csproj"
copy . .
workdir "/src/identityservice.api"
run dotnet build "./identityservice.api.csproj" -c $build_configuration -o /app/build

# this stage is used to publish the service project to be copied to the final stage
from build as publish
arg build_configuration=release
run dotnet publish "./identityservice.api.csproj" -c $build_configuration -o /app/publish /p:useapphost=false

# this stage is used in production or when running from vs in regular mode (default when not using the debug configuration)
from base as final
workdir /app
copy --from=publish /app/publish .
entrypoint ["dotnet", "identityservice.api.dll"]