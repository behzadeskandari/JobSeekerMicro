using System.Text;
using AdvertisementService.Api.Common;
using AdvertisementService.Api.Filters;
using AdvertisementService.Api.Middleware;
using AdvertisementService.Application.Common.Interfaces;
using AdvertisementService.Infrastructure;
using AdvertisementService.Infrastructure.Services;
using AdvertisementService.Persistence;
using AdvertisementService.Persistence.DbContexts;
using JobSeeker.Shared.Contracts.Integration;
using JobSeeker.Shared.Kernel.Middleware;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProfileService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Register CurrentUserService
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

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
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
    });

// Register ProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory, JobSeekerProblemDetailsFactory>();

builder.Services.AddDbContext<AdvertismentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register persistence services
builder.Services.AddAdvertismentPersistanceServiceRegistration(builder.Configuration);

// Register infrastructure services (HTTP clients)
builder.Services.ConfigureAdvertismentInfrastructureServiceRegistration(builder.Configuration);

// MassTransit configuration for publishing integration events
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // Publish endpoints for integration events
        cfg.Publish<AdvertisementCreatedIntegrationEvent>(p => { p.Durable = true; });
        cfg.Publish<PaymentProcessedIntegrationEvent>(p => { p.Durable = true; });
        cfg.Publish<OrderPlacedIntegrationEvent>(p => { p.Durable = true; });
        cfg.Publish<ProductInventoryUpdatedIntegrationEvent>(p => { p.Durable = true; });
        cfg.Publish<SalesOrderCreatedIntegrationEvent>(p => { p.Durable = true; });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add error handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ResterictAccessMiddleware>();

app.Run();
