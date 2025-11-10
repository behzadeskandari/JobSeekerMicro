using System.Reflection;
using System.Threading.RateLimiting;
using AppJob.Core.Configuration;
using AppJob.Core.Services;
using AspNetCoreRateLimit;
using IdentityService.Api.Filters;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Services;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure;
using IdentityService.Persistence.DbContext;
using JobSeeker.Shared.Kernel.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(
//c =>
//{
//    c.SchemaGeneratorOptions = new SchemaGeneratorOptions
//    {
//        SchemaIdSelector = type => type.FullName
//    };
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GateWay api", Version = "V1" });

//    // Set the comments path for the Swagger JSON and UI.
//    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
//}
//);
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SchemaGeneratorOptions = new SchemaGeneratorOptions
        {
            SchemaIdSelector = type => type.FullName
        };
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "GateWay Api ", Version = "V1" });

        // Include XML comments
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

        // ✅ Keep only ONE Bearer definition block
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme."
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
        c.OperationFilter<SwaggerExcludeAuthOperationFilter>();
    });


builder.Services.AddDbContext<ApplicationUserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddIdentity<User, IdentityRole>()
//    .AddDefaultTokenProviders()
//    .AddEntityFrameworkStores<ApplicationUserDbContext>().AddRoles<IdentityRole>();


builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = true;

})
.AddRoles<IdentityRole>() //adds roles 
.AddRoleManager<RoleManager<IdentityRole>>() //be able to use of role manager 
.AddEntityFrameworkStores<ApplicationUserDbContext>() //providing our context 
.AddSignInManager<SignInManager<User>>() ////make use of sigin manager 
.AddUserManager<UserManager<User>>() //make use of usemanager  to create user
.AddDefaultTokenProviders(); //be abe to create tokens for email confimation 



builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

});


builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.RegisterAppJobServicesApp(builder.Configuration);
builder.Services.AddScoped<ICommunicationOrchestrator, CommunicationOrchestrator>();
builder.Services.AddAuthorization();
builder.Services.AddMemoryCache();
builder.Services.ConfigureInfrastructureRegistrationServices(builder.Configuration);
builder.Services.AddJWTService(builder.Configuration);


builder.Services.AddOptions<RateLimitOptions>()
        .Bind(builder.Configuration.GetSection("RateLimiting"))
        .ValidateDataAnnotations();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 100;
        options.QueueProcessingOrder = QueueProcessingOrder.NewestFirst;
        options.QueueLimit = 70;
        options.Window = TimeSpan.FromMinutes(1);
    });
});

builder.Services.AddCors(builder =>
{
    builder.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});




builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddInMemoryRateLimiting();

builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.GeneralRules = new List<RateLimitRule> {
        new RateLimitRule()
        {
            Endpoint = "*",
            Limit= 100,
            Period = "1m"
        }
    };
});
builder.Services.Configure<RateLimitOptions>(options =>
{
    options.GeneralRules.Add(new RateLimitRule()
    {
        Endpoint = "*",
        Limit = 100,
        Period = "1m",
        MonitorMode = true,
        PeriodTimespan = TimeSpan.FromSeconds(60),
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIs V 01");
        app.UseDeveloperExceptionPage();
        //c.InjectStylesheet("/Content/Swagger.css");
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseMiddleware<ResterictAccessMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseIpRateLimiting();
app.UseRateLimiter();




app.Run();
// Extension method
