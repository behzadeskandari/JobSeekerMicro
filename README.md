# JobSeekerMicro

A clean DDD/CQRS/Onion Architecture .NET microservices solution with 5 services: AdvertisementService, IdentityService, JobService, ProfileService, AssessmentService.

## Prerequisites

- .NET 8.0 SDK
- Local SQL Server (or SQL Server Express) running on localhost:1433
- Docker (for containerized deployment)

## Local Development with Docker

Each service can be run independently using Docker. Make sure you have a local SQL Server instance running.

### Prerequisites for Docker Development

1. Install Docker Desktop
2. Ensure SQL Server is running locally on port 1433 with SA user
3. Update the connection string password in the commands below if different

### Running Individual Services

#### AdvertisementService
```bash
cd AdvertisementService.Api
docker build -t advertisementservice-api .
docker run -d -p 5004:80 --name ads-api -e ASPNETCORE_ENVIRONMENT=Development -e ConnectionStrings__DefaultConnection="Server=host.docker.internal,1433;Database=AdvertisementDB;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;" advertisementservice-api
```

#### IdentityService
```bash
cd IdentityService.Api
docker build -t identityservice-api .
docker run -d -p 5001:80 --name identity-api -e ASPNETCORE_ENVIRONMENT=Development -e ConnectionStrings__DefaultConnection="Server=host.docker.internal,1433;Database=IdentityDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;" identityservice-api
```

#### JobService
```bash
cd JobService.Api
docker build -t jobservice-api .
docker run -d -p 5003:80 --name job-api -e ASPNETCORE_ENVIRONMENT=Development -e ConnectionStrings__DefaultConnection="Server=host.docker.internal,1433;Database=JobServiceDB;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;" jobservice-api
```

#### ProfileService
```bash
cd ProfileService.Api
docker build -t profileservice-api .
docker run -d -p 5002:80 --name profile-api -e ASPNETCORE_ENVIRONMENT=Development -e ConnectionStrings__DefaultConnection="Server=host.docker.internal,1433;Database=ProfileServiceDB;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;" profileservice-api
```

#### AssessmentService
```bash
cd AssessmentService.Api
docker build -t assessmentservice-api .
docker run -d -p 5005:80 --name assessment-api -e ASPNETCORE_ENVIRONMENT=Development -e ConnectionStrings__DefaultConnection="Server=host.docker.internal,1433;Database=AssessmentServiceDB;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;" assessmentservice-api
```

### Running All Services with Docker Compose

```bash
docker-compose up --build
```

This will start all services defined in docker-compose.yml. Note that the DB and RabbitMQ services are commented out as they use external/local instances.

### Health Checks

Each service includes health check endpoints. You can check service health at:
- `http://localhost:{port}/health`

### Development with Hot Reload

For development with hot reload, use the docker-compose.yml which includes volume mounts for the API projects.

## Architecture

This solution follows Clean Architecture principles with:
- Domain-Driven Design (DDD)
- Command Query Responsibility Segregation (CQRS)
- Onion Architecture
- Microservices pattern

## Services Overview

- **IdentityService**: Handles user authentication and authorization
- **ProfileService**: Manages user profiles and related data
- **JobService**: Handles job postings and applications
- **AdvertisementService**: Manages advertisements and promotions
- **AssessmentService**: Handles user assessments and evaluations

## Database Migrations

Each service has its own database. To run migrations:

```bash
cd {ServiceName}.Api
dotnet ef database update --context {DbContextName}
```

Or using Package Manager Console in Visual Studio:

```powershell
Update-Database -Context {DbContextName} -Project {ServiceName}.Persistence
```

## Event Bus Migration

### Overview
All microservices now use a single, consistent message broker setup via the shared `JobSeeker.Shared.EventBusRabbitMQ` library. This replaces the previous per-service RabbitMQ queue approach with a unified pub-sub model.

### Key Changes
- **Single Queue**: All services now publish to and consume from the `jobseeker-events` queue
- **Direct Exchange**: Uses `jobseeker-exchange` as a direct exchange bound to the shared queue
- **Shared Library**: All event bus functionality is centralized in `JobSeeker.Shared.EventBusRabbitMQ`
- **Configuration**: RabbitMQ connection settings are configured in each service's `appsettings.json`

### Configuration
Each service's `appsettings.json` now includes a `RabbitMQ` section:

```json
{
  "RabbitMQ": {
    "HostName": "localhost",
    "UserName": "guest",
    "Password": "guest"
  }
}
```

### Docker Setup
The `docker-compose.yml` now includes a single RabbitMQ container that all services use. Uncomment the RabbitMQ service section if you're using Docker for local development.

### Backward Compatibility
This change maintains backward compatibility with existing integration events. The shared library supports all existing event types defined in `JobSeeker.Shared.Contracts`.

### Sample Event Handler
A sample event handler has been added to `AssessmentService` that demonstrates how to consume events from the shared queue. The handler logs `JobApplicationSubmittedIntegrationEvent` events.

### Publishing Events
To publish events in your service:

```csharp
// Inject IEventBus in your service
private readonly IEventBus _eventBus;

// Publish an event
await _eventBus.PublishAsync(new JobApplicationSubmittedIntegrationEvent(jobApplicationId, jobPostId, userId, resumeUrl, appliedAt));
```

### Consuming Events
To consume events in your service:

1. Create an event handler implementing `IIntegrationEventHandler<T>`
2. Register the handler in `Program.cs`
3. Subscribe to the event during application startup

```csharp
// Register handler
builder.Services.AddScoped<IIntegrationEventHandler<MyEvent>, MyEventHandler>();

// Subscribe during startup
await eventBus.SubscribeAsync<MyEvent, MyEventHandler>();
```

