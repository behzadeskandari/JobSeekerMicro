using AssessmentService.Persistance;
using AssessmentService.Persistance.DbContexts;
using AssessmentService.Persistance.SeedData;
using JobSeeker.Shared.Kernel.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<AssessmentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register persistence services
builder.Services.AddAssessmentPersistanceServiceRegistration(builder.Configuration);

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

// Initialize and seed database
await SeedDataAssessment.InitializeAsync(app.Services);
await SeedDataAssessment.SeedAsync(app.Services);

app.Run();
