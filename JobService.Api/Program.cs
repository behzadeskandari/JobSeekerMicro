using JobSeeker.Shared.Contracts.Integration;
using JobSeeker.Shared.Contracts.IntegrationEvents;
using JobSeeker.Shared.Kernel.Middleware;
using JobService.Application;
using JobService.Infrastructure;
using JobService.Persistence;
using JobService.Persistence.DbContexts;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ResterictAccessMiddleware>();


app.Run();
