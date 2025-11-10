using System.Reflection;
using System.Text;
using GateWay.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);



builder.Services.AddAntiforgery(x =>
{
    x.HeaderName = "X-CSRF-TOKEN"; // Set the header name for CSRF token
    x.Cookie.Name = "X-CSRF-TOKEN"; // Set the cookie name for CSRF token
    x.SuppressXFrameOptionsHeader = false;
    x.Cookie.HttpOnly = false;
});


builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SchemaGeneratorOptions = new SchemaGeneratorOptions
        {
            SchemaIdSelector = type => type.FullName
        };
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobMicro", Version = "V1" });

        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("Bearer", options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:5000", // Your Identity Server URL or issuer
        ValidAudience = "jobseeker_api", // Your API resource name or audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YOUR_SECRET_HERE")) // Your signing key or retrieve from config

    };
});

builder.Services.AddCors(options=>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.ConfigureSwaggerGen(options =>
{
    options.IncludeXmlComments($"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",

    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
});

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";  // Default path
}, x =>
{
    x.RoutePrefix = "swagger"; // Set Swagger UI at /swagger
});
app.UseRouting();
app.UseCors();
app.MapFallback(() => Results.NotFound());
app.UseHttpsRedirection();
app.UseMiddleware<InterceptionMiddleware>();
app.UseMiddleware<TokenCheckerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await app.UseOcelot();
app.Run();
