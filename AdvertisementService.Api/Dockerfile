# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY ["AdvertisementService.Api/AdvertisementService.Api.csproj", "AdvertisementService.Api/"]
COPY ["AdvertisementService.Application/AdvertisementService.Application.csproj", "AdvertisementService.Application/"]
COPY ["AdvertisementService.Domain/AdvertisementService.Domain.csproj", "AdvertisementService.Domain/"]
COPY ["AdvertisementService.Persistence/AdvertisementService.Persistence.csproj", "AdvertisementService.Persistence/"]
COPY ["AdvertisementService.Infrastructure/AdvertisementService.Infrastructure.csproj", "AdvertisementService.Infrastructure/"]
COPY ["JobSeeker.Shared.Contracts/JobSeeker.Shared.Contracts.csproj", "JobSeeker.Shared.Contracts/"]
COPY ["JobSeeker.Shared.Kernel/JobSeeker.Shared.Kernel.csproj", "JobSeeker.Shared.Kernel/"]

RUN dotnet restore "AdvertisementService.Api/AdvertisementService.Api.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/AdvertisementService.Api"
RUN dotnet build "AdvertisementService.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "AdvertisementService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 CMD curl --fail http://localhost/health || exit 1
LABEL version="1.0" maintainer="behzadeskandari"
ENTRYPOINT ["dotnet", "AdvertisementService.Api.dll"]