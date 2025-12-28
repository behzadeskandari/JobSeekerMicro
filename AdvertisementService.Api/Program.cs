using AdvertisementService.Infrastructure;
using AdvertisementService.Persistence;
using AdvertisementService.Persistence.DbContexts;
using JobSeeker.Shared.Contracts.Integration;
using JobSeeker.Shared.Kernel.Middleware;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProfileService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ResterictAccessMiddleware>();

app.Run();
