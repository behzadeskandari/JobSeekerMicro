# Example for IdentityService.Api/Dockerfile (adapt for others)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Restore (copies csproj for layer caching)
COPY ["IdentityService.Api/IdentityService.Api.csproj", "."]
COPY ["../IdentityService.Application/IdentityService.Application.csproj", "../IdentityService.Application/"]
COPY ["../IdentityService.Domain/IdentityService.Domain.csproj", "../IdentityService.Domain/"]
COPY ["../IdentityService.Infrastructure/IdentityService.Infrastructure.csproj", "../IdentityService.Infrastructure/"]
COPY ["../IdentityService.Persistence/IdentityService.Persistence.csproj", "../IdentityService.Persistence/"]
# Shared (root-level paths relative to .Api)
COPY ["../../JobSeeker.Shared.Common/JobSeeker.Shared.Common.csproj", "../../JobSeeker.Shared.Common/"]
COPY ["../../JobSeeker.Shared.Contracts/JobSeeker.Shared.Contracts.csproj", "../../JobSeeker.Shared.Contracts/"]
COPY ["../../JobSeeker.Shared.EventBusRabbitMQ/JobSeeker.Shared.EventBusRabbitMQ.csproj", "../../JobSeeker.Shared.EventBusRabbitMQ/"]
COPY ["../../JobSeeker.Shared.Kernel/JobSeeker.Shared.Kernel.csproj", "../../JobSeeker.Shared.Kernel/"]
COPY ["../../JobSeeker.Shared.Logging/JobSeeker.Shared.Logging.csproj", "../../JobSeeker.Shared.Logging/"]
COPY ["../../JobSeeker.Shared.Models/JobSeeker.Shared.Models.csproj", "../../JobSeeker.Shared.Models/"]
RUN dotnet restore "IdentityService.Api/IdentityService.Api.csproj"

# Build
COPY . .
WORKDIR "/src/IdentityService.Api"
RUN dotnet build "IdentityService.Api.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "IdentityService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "IdentityService.Api.dll"]