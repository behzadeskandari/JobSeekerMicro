# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY ["IdentityService.Api/IdentityService.Api.csproj", "IdentityService.Api/"]
COPY ["AppJob.Core/AppJob.Core.csproj", "AppJob.Core/"]
COPY ["IdentityService.Application/IdentityService.Application.csproj", "IdentityService.Application/"]
COPY ["IdentityService.Domain/IdentityService.Domain.csproj", "IdentityService.Domain/"]
COPY ["IdentityService.Persistence/IdentityService.Persistence.csproj", "IdentityService.Persistence/"]
COPY ["IdentityService.Infrastructure/IdentityService.Infrastructure.csproj", "IdentityService.Infrastructure/"]
COPY ["JobSeeker.Shared.Common/JobSeeker.Shared.Common.csproj", "JobSeeker.Shared.Common/"]
COPY ["JobSeeker.Shared.Kernel/JobSeeker.Shared.Kernel.csproj", "JobSeeker.Shared.Kernel/"]

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
EXPOSE 80
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 CMD curl --fail http://localhost/health || exit 1
LABEL version="1.0" maintainer="behzadeskandari"
ENTRYPOINT ["dotnet", "IdentityService.Api.dll"]