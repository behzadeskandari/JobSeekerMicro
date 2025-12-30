using System.Text;
using AssessmentService.Api.Common;
using AssessmentService.Api.Filters;
using AssessmentService.Api.Middleware;
using AssessmentService.Application.Common.Interfaces;
using AssessmentService.Infrastructure.Services;
using AssessmentService.Persistance;
using AssessmentService.Persistance.DbContexts;
using AssessmentService.Persistance.SeedData;
using JobSeeker.Shared.Contracts.IntegrationEvents;
using JobSeeker.Shared.Kernel.Middleware;
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

builder.Services.AddAuthorization();

// Register ProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory, JobSeekerProblemDetailsFactory>();

// Add DbContext
builder.Services.AddDbContext<AssessmentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register persistence services
builder.Services.AddAssessmentPersistanceServiceRegistration(builder.Configuration);

// MassTransit configuration for request-response pattern
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

// Register request client for GetUserById
builder.Services.AddScoped<IRequestClient<GetUserByIdRequestIntegrationEvent>>(provider =>
    provider.GetRequiredService<IBus>().CreateRequestClient<GetUserByIdRequestIntegrationEvent>(
        TimeSpan.FromSeconds(10)
    ));

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

// Initialize and seed database
await SeedDataAssessment.InitializeAsync(app.Services);
await SeedDataAssessment.SeedAsync(app.Services);

app.Run();
