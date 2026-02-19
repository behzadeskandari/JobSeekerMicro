# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY ["JobService.Api/JobService.Api.csproj", "JobService.Api/"]
COPY ["JobSeeker.Shared.Kernel/JobSeeker.Shared.Kernel.csproj", "JobSeeker.Shared.Kernel/"]
COPY ["JobSeeker.Shared.EventBusRabbitMQ/JobSeeker.Shared.EventBusRabbitMQ.csproj", "JobSeeker.Shared.EventBusRabbitMQ/"]
COPY ["JobService.Application/JobService.Application.csproj", "JobService.Application/"]
COPY ["JobService.Domain/JobService.Domain.csproj", "JobService.Domain/"]
COPY ["JobService.Persistence/JobService.Persistence.csproj", "JobService.Persistence/"]
COPY ["JobService.Infrastructure/JobService.Infrastructure.csproj", "JobService.Infrastructure/"]

RUN dotnet restore "JobService.Api/JobService.Api.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/JobService.Api"
RUN dotnet build "JobService.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "JobService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 CMD curl --fail http://localhost/health || exit 1
LABEL version="1.0" maintainer="behzadeskandari"
ENTRYPOINT ["dotnet", "JobService.Api.dll"]