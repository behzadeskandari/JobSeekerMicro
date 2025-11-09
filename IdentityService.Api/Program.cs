using AppJob.Core.Configuration;
using AppJob.Core.Services;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Services;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure;
using IdentityService.Persistence.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationUserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationUserDbContext>().AddRoles<IdentityRole>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.RegisterAppJobServicesApp(builder.Configuration);
builder.Services.AddScoped<ICommunicationOrchestrator, CommunicationOrchestrator>();
builder.Services.AddAuthorization();
builder.Services.AddMemoryCache();
builder.Services.ConfigureInfrastructureRegistrationServices(builder.Configuration);
builder.Services.AddJWTService(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication().UseAuthorization();



app.MapControllers();

app.Run();
