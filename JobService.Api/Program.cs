using System.Text;
using JobSeeker.Shared.Common.SeedData;
using JobSeeker.Shared.Contracts.Integration;
using JobSeeker.Shared.Contracts.IntegrationEvents;
using JobSeeker.Shared.Kernel.Middleware;
using JobService.Api.Common;
using JobService.Api.Filters;
using JobService.Api.Middleware;
using JobService.Application;
using JobService.Application.Common.Interfaces;
using JobService.Infrastructure;
using JobService.Infrastructure.Services;
using JobService.Persistence;
using JobService.Persistence.DbContexts;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        options.RequireHttpsMetadata = false; // Set to true in production
        options.SaveToken = true;
    });

// Register ProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory, JobSeekerProblemDetailsFactory>();

// Add DbContext
builder.Services.AddDbContext<JobDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register persistence services
builder.Services.AddJobApplicationServiceRegistration()
    .AddJobPersistanceService(builder.Configuration)
    .AddJobInfrastructureService(builder.Configuration);


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
        cfg.Publish<JobPostPublishedIntegrationEvent>(p => { p.Durable = true; });
        cfg.Publish<JobApplicationSubmittedIntegrationEvent>(p => { p.Durable = true; });
        cfg.Publish<CompanyCreatedIntegrationEvent>(p => { p.Durable = true; });
        cfg.Publish<OfferSentIntegrationEvent>(p => { p.Durable = true; });
        cfg.Publish<InterviewScheduledIntegrationEvent>(p => { p.Durable = true; });
        cfg.Publish<JobApplicationRejectedIntegrationEvent>(p => { p.Durable = true; });
        cfg.Publish<JobSavedIntegrationEvent>(p => { p.Durable = true; });

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

// Add error handling middleware (before authentication/authorization)
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ResterictAccessMiddleware>();

await SeedDataJob.InitializeAsync(app.Services);
await SeedDataJob.SeedAsync(app.Services);

app.Run();
