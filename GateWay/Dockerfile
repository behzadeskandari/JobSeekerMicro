FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Restore: Copy csproj files
COPY ["GateWay/GateWay.csproj", "GateWay/"]
COPY ["JobSeeker.Shared.Common/JobSeeker.Shared.Common.csproj", "JobSeeker.Shared.Common/"]
COPY ["JobSeeker.Shared.Contracts/JobSeeker.Shared.Contracts.csproj", "JobSeeker.Shared.Contracts/"]
COPY ["JobSeeker.Shared.Kernel/JobSeeker.Shared.Kernel.csproj", "JobSeeker.Shared.Kernel/"]
COPY ["JobSeeker.Shared.Logging/JobSeeker.Shared.Logging.csproj", "JobSeeker.Shared.Logging/"]
RUN dotnet restore "GateWay/GateWay.csproj"

# Build: Copy GateWay code/config (Dockerfile excluded via .dockerignore)
COPY GateWay/ GateWay/

# Copy shared source
COPY JobSeeker.Shared.Common/ JobSeeker.Shared.Common/
COPY JobSeeker.Shared.Contracts/ JobSeeker.Shared.Contracts/
COPY JobSeeker.Shared.Kernel/ JobSeeker.Shared.Kernel/
COPY JobSeeker.Shared.Logging/ JobSeeker.Shared.Logging/

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