using System.Text;
using JobSeeker.Shared.Contracts.Integration;
using JobSeeker.Shared.Contracts.IntegrationEvents;
using JobSeeker.Shared.EventBusRabbitMQ;
using JobSeeker.Shared.Kernel.Middleware;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ProfileService.Api.Common;
using ProfileService.Api.Filters;
using ProfileService.Api.Middleware;
using ProfileService.Application.Features.UserSettings.Queries;
using ProfileService.Infrastructure.Interfaces;
using ProfileService.Infrastructure.Services;
using ProfileService.Persistance;
using ProfileService.Persistance.DbContexts;
using ProfileService.Persistance.Messaging.Consumers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllUserSettingsQuery).Assembly));

// Add DbContext
builder.Services.AddDbContext<ProfileServiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register persistence services
builder.Services.AddProfilePersistanceServiceRegistration(builder.Configuration);

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Register CurrentUserService
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// Configure event bus using shared library
var rabbitMqHost = builder.Configuration.GetSection("RabbitMQ")["HostName"] ?? "localhost";
var rabbitMqUser = builder.Configuration.GetSection("RabbitMQ")["UserName"] ?? "guest";
var rabbitMqPassword = builder.Configuration.GetSection("RabbitMQ")["Password"] ?? "guest";
var rabbitMqConnectionString = $"amqp://{rabbitMqUser}:{rabbitMqPassword}@{rabbitMqHost}:5672/";

builder.Services.AddMassTransit(x =>
{
    // register consumers if you have any: x.AddConsumer<MyConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqHost, "/", h =>
        {
            h.Username(rabbitMqUser);
            h.Password(rabbitMqPassword);
        });
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddMassTransitHostedService();
builder.Services.AddEventBusRabbitMQ(
    connectionString: rabbitMqConnectionString,
    queueName: "jobseeker-events",
    exchangeName: "jobseeker-exchange");

// Register event handlers
builder.Services.AddScoped<JobSeeker.Shared.EventBusRabbitMQ.IIntegrationEventHandler<UserRegisteredIntegrationEvent>, UserRegisteredConsumer>();
builder.Services.AddScoped<JobSeeker.Shared.EventBusRabbitMQ.IIntegrationEventHandler<JobPostPublishedIntegrationEvent>, ProfileService.Application.IntegrationEvents.JobPostPublishedEventHandler>();



// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"] ?? builder.Configuration["IdentityServiceUrl"],
            ValidAudience = builder.Configuration["JWT:Audience"] ?? "jobseeker.api",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? throw new InvalidOperationException("JWT:Key is required"))),
            ClockSkew = TimeSpan.Zero
        };
        options.RequireHttpsMetadata = false; // Set to true in production
        options.SaveToken = true;
    });

builder.Services.AddAuthorization();

// Register ProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory, JobSeekerProblemDetailsFactory>();

var app = builder.Build();





// In Program.cs after building app
using (var scope = app.Services.CreateScope())
{
    try
    {
        var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
        await eventBus.SubscribeAsync<UserRegisteredIntegrationEvent, UserRegisteredConsumer>();
        eventBus.StartConsuming();

        Serilog.Log.Information("Event bus started successfully");
    }
    catch (Exception ex)
    {
        Serilog.Log.Error(ex, "Failed to start event bus");
        throw;
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add error handling middleware (before authentication/authorization)
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<ResterictAccessMiddleware>();

// Start event bus consumer
using (var scope = app.Services.CreateScope())
{
    var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
    await eventBus.SubscribeAsync<UserRegisteredIntegrationEvent, UserRegisteredConsumer>();
    await eventBus.SubscribeAsync<JobPostPublishedIntegrationEvent, ProfileService.Application.IntegrationEvents.JobPostPublishedEventHandler>();
    eventBus.StartConsuming();
}

app.Run();
