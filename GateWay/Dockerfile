FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Restore: Copy csproj files from root (context is .)
COPY ["GateWay/GateWay.csproj", "GateWay/"]
# Shared projects (direct from root)
COPY ["JobSeeker.Shared.Common/JobSeeker.Shared.Common.csproj", "JobSeeker.Shared.Common/"]
COPY ["JobSeeker.Shared.Contracts/JobSeeker.Shared.Contracts.csproj", "JobSeeker.Shared.Contracts/"]
COPY ["JobSeeker.Shared.Logging/JobSeeker.Shared.Logging.csproj", "JobSeeker.Shared.Logging/"]
# Add only if referenced (check GateWay.csproj; e.g., for RabbitMQ)
# COPY ["JobSeeker.Shared.EventBusRabbitMQ/JobSeeker.Shared.EventBusRabbitMQ.csproj", "JobSeeker.Shared.EventBusRabbitMQ/"]
RUN dotnet restore "GateWay/GateWay.csproj"  # Restore from GateWay subdir

# Build: Copy full GateWay folder and shared source
COPY ["GateWay/", "GateWay/"]
COPY ["JobSeeker.Shared.Common/", "JobSeeker.Shared.Common/"]
COPY ["JobSeeker.Shared.Contracts/", "JobSeeker.Shared.Contracts/"]
COPY ["JobSeeker.Shared.Logging/", "JobSeeker.Shared.Logging/"]
# Copy more shared source if needed
WORKDIR "/src/GateWay"
RUN dotnet build "GateWay.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
WORKDIR "/src/GateWay"
RUN dotnet publish "GateWay.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "GateWay.dll"]