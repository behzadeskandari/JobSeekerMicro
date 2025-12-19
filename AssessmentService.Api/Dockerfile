FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AssessmentService.Api.csproj", "."]
COPY ["../AssessmentService.Application/AssessmentService.Application.csproj", "../AssessmentService.Application/"]
COPY ["../AssessmentService.Domain/AssessmentService.Domain.csproj", "../AssessmentService.Domain/"]
COPY ["../AssessmentService.Infrastructure/AssessmentService.Infrastructure.csproj", "../AssessmentService.Infrastructure/"]
COPY ["../AssessmentService.Persistence/AssessmentService.Persistence.csproj", "../AssessmentService.Persistence/"]
COPY ["../../JobSeeker.Shared.Common/JobSeeker.Shared.Common.csproj", "../../JobSeeker.Shared.Common/"]
COPY ["../../JobSeeker.Shared.Contracts/JobSeeker.Shared.Contracts.csproj", "../../JobSeeker.Shared.Contracts/"]
COPY ["../../JobSeeker.Shared.EventBusRabbitMQ/JobSeeker.Shared.EventBusRabbitMQ.csproj", "../../JobSeeker.Shared.EventBusRabbitMQ/"]
COPY ["../../JobSeeker.Shared.Kernel/JobSeeker.Shared.Kernel.csproj", "../../JobSeeker.Shared.Kernel/"]
COPY ["../../JobSeeker.Shared.Logging/JobSeeker.Shared.Logging.csproj", "../../JobSeeker.Shared.Logging/"]
COPY ["../../JobSeeker.Shared.Models/JobSeeker.Shared.Models.csproj", "../../JobSeeker.Shared.Models/"]
RUN dotnet restore "AssessmentService.Api.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "AssessmentService.Api.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "AssessmentService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "AssessmentService.Api.dll"]