# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY ["AssessmentService.Api/AssessmentService.Api.csproj", "AssessmentService.Api/"]
COPY ["AssessmentService.Application/AssessmentService.Application.csproj", "AssessmentService.Application/"]
COPY ["AssessmentService.Domain/AssessmentService.Domain.csproj", "AssessmentService.Domain/"]
COPY ["AssessmentService.Infrastructure/AssessmentService.Infrastructure.csproj", "AssessmentService.Infrastructure/"]
COPY ["AssessmentService.Persistance/AssessmentService.Persistance.csproj", "AssessmentService.Persistance/"]
COPY ["JobSeeker.Shared.Kernel/JobSeeker.Shared.Kernel.csproj", "JobSeeker.Shared.Kernel/"]

RUN dotnet restore "AssessmentService.Api/AssessmentService.Api.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/AssessmentService.Api"
RUN dotnet build "AssessmentService.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "AssessmentService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 CMD curl --fail http://localhost/health || exit 1
LABEL version="1.0" maintainer="behzadeskandari"
ENTRYPOINT ["dotnet", "AssessmentService.Api.dll"]