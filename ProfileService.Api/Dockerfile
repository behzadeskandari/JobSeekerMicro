FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ProfileService.Api.csproj", "."]
COPY ["../ProfileService.Application/ProfileService.Application.csproj", "../ProfileService.Application/"]
COPY ["../ProfileService.Domain/ProfileService.Domain.csproj", "../ProfileService.Domain/"]
COPY ["../ProfileService.Infrastructure/ProfileService.Infrastructure.csproj", "../ProfileService.Infrastructure/"]
COPY ["../ProfileService.Persistance/ProfileService.Persistance.csproj", "../ProfileService.Persistance/"]
COPY ["../../JobSeeker.Shared.Common/JobSeeker.Shared.Common.csproj", "../../JobSeeker.Shared.Common/"]
COPY ["../../JobSeeker.Shared.Contracts/JobSeeker.Shared.Contracts.csproj", "../../JobSeeker.Shared.Contracts/"]
RUN dotnet restore "ProfileService.Api.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "ProfileService.Api.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "ProfileService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "ProfileService.Api.dll"]