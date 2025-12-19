FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Restore: Copy csproj files for layer caching
COPY ["ProfileService.Api.csproj", "."]
COPY ["../ProfileService.Application/ProfileService.Application.csproj", "../ProfileService.Application/"]
COPY ["../ProfileService.Domain/ProfileService.Domain.csproj", "../ProfileService.Domain/"]
COPY ["../ProfileService.Infrastructure/ProfileService.Infrastructure.csproj", "../ProfileService.Infrastructure/"]
COPY ["../ProfileService.Persistence/ProfileService.Persistence.csproj", "../ProfileService.Persistence/"]
# Shared projects
COPY ["../../JobSeeker.Shared.Common/JobSeeker.Shared.Common.csproj", "../../JobSeeker.Shared.Common/"]
COPY ["../../JobSeeker.Shared.Contracts/JobSeeker.Shared.Contracts.csproj", "../../JobSeeker.Shared.Contracts/"]
COPY ["../../JobSeeker.Shared.EventBusRabbitMQ/JobSeeker.Shared.EventBusRabbitMQ.csproj", "../../JobSeeker.Shared.EventBusRabbitMQ/"]
COPY ["../../JobSeeker.Shared.Kernel/JobSeeker.Shared.Kernel.csproj", "../../JobSeeker.Shared.Kernel/"]
COPY ["../../JobSeeker.Shared.Logging/JobSeeker.Shared.Logging.csproj", "../../JobSeeker.Shared.Logging/"]
COPY ["../../JobSeeker.Shared.Models/JobSeeker.Shared.Models.csproj", "../../JobSeeker.Shared.Models/"]
RUN dotnet restore "ProfileService.Api.csproj"

# Build
COPY . .
WORKDIR "/src"
RUN dotnet build "ProfileService.Api.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "ProfileService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "ProfileService.Api.dll"]