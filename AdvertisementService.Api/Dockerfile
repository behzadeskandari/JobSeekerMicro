FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Restore: Copy csproj files for layer caching
COPY ["AdvertisementService.Api.csproj", "."]
COPY ["../AdvertisementService.Application/AdvertisementService.Application.csproj", "../AdvertisementService.Application/"]
COPY ["../AdvertisementService.Domain/AdvertisementService.Domain.csproj", "../AdvertisementService.Domain/"]
COPY ["../AdvertisementService.Infrastructure/AdvertisementService.Infrastructure.csproj", "../AdvertisementService.Infrastructure/"]
COPY ["../AdvertisementService.Persistence/AdvertisementService.Persistence.csproj", "../AdvertisementService.Persistence/"]
# Shared projects
COPY ["../../JobSeeker.Shared.Common/JobSeeker.Shared.Common.csproj", "../../JobSeeker.Shared.Common/"]
COPY ["../../JobSeeker.Shared.Contracts/JobSeeker.Shared.Contracts.csproj", "../../JobSeeker.Shared.Contracts/"]
COPY ["../../JobSeeker.Shared.EventBusRabbitMQ/JobSeeker.Shared.EventBusRabbitMQ.csproj", "../../JobSeeker.Shared.EventBusRabbitMQ/"]
COPY ["../../JobSeeker.Shared.Kernel/JobSeeker.Shared.Kernel.csproj", "../../JobSeeker.Shared.Kernel/"]
COPY ["../../JobSeeker.Shared.Logging/JobSeeker.Shared.Logging.csproj", "../../JobSeeker.Shared.Logging/"]
COPY ["../../JobSeeker.Shared.Models/JobSeeker.Shared.Models.csproj", "../../JobSeeker.Shared.Models/"]
RUN dotnet restore "AdvertisementService.Api.csproj"

# Build
COPY . .
WORKDIR "/src"
RUN dotnet build "AdvertisementService.Api.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "AdvertisementService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "AdvertisementService.Api.dll"]