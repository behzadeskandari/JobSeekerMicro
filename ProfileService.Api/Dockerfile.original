# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY ["ProfileService.Api/ProfileService.Api.csproj", "ProfileService.Api/"]
COPY ["JobSeeker.Shared.Kernel/JobSeeker.Shared.Kernel.csproj", "JobSeeker.Shared.Kernel/"]
COPY ["ProfileService.Application/ProfileService.Application.csproj", "ProfileService.Application/"]
COPY ["ProfileService.Domain/ProfileService.Domain.csproj", "ProfileService.Domain/"]
COPY ["ProfileService.Persistance/ProfileService.Persistance.csproj", "ProfileService.Persistance/"]

RUN dotnet restore "ProfileService.Api/ProfileService.Api.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/ProfileService.Api"
RUN dotnet build "ProfileService.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "ProfileService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 CMD curl --fail http://localhost/health || exit 1
LABEL version="1.0" maintainer="behzadeskandari"
ENTRYPOINT ["dotnet", "ProfileService.Api.dll"]