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

