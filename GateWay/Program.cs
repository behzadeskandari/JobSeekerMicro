//using System.Reflection;
//using System.Text;
//using System.Text.Json.Serialization;
//using GateWay.Middleware;
//using JobSeeker.Shared.Kernel.Middleware;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using Ocelot.DependencyInjection;
//using Ocelot.Middleware;
//using Swashbuckle.AspNetCore.SwaggerGen;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
////builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();
//builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

//builder.Services.AddOcelot(builder.Configuration);
//builder.Services.AddSwaggerForOcelot(builder.Configuration);
//builder.Services.AddControllers().AddJsonOptions(options =>
//{

//    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//}); ;

//builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen(
////c =>
////{
////    c.SchemaGeneratorOptions = new SchemaGeneratorOptions
////    {
////        SchemaIdSelector = type => type.FullName
////    };
////    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GateWay api", Version = "V1" });

////    // Set the comments path for the Swagger JSON and UI.
////    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
////    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
////    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
////}
////);
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme =
//    options.DefaultChallengeScheme =
//    options.DefaultForbidScheme =
//    options.DefaultScheme =
//    options.DefaultSignInScheme =
//    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

//})
//.AddJwtBearer(options =>
//{

//    options.RequireHttpsMetadata = false;
//    options.SaveToken = false;
//    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
//        ValidIssuer = builder.Configuration["JWT:Issuer"],
//        ValidAudience = builder.Configuration["JWT:Audience"]
//    };
//    options.Events = new JwtBearerEvents
//    {
//        OnAuthenticationFailed = context =>
//        {
//            Console.WriteLine("Authentication failed: " + context.Exception.Message);
//            return Task.CompletedTask;
//        },
//        OnTokenValidated = context =>
//        {
//            Console.WriteLine("Token validated successfully.");
//            return Task.CompletedTask;
//        },
//        OnMessageReceived = context =>
//        {
//            return Task.CompletedTask;
//        }
//    };
//});




//var tokenValidationParams = new TokenValidationParameters
//{
//    ValidateIssuer = true,
//    ValidateAudience = true,
//    ValidateLifetime = true,
//    ValidateIssuerSigningKey = true,
//    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
//    ValidIssuer = builder.Configuration["JWT:Issuer"],
//    ValidAudience = builder.Configuration["JWT:Audience"]
//};

//builder.Services.AddSingleton(tokenValidationParams);



////builder.Services.AddAuthentication(options =>
////{
////    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
////    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
////})
////.AddJwtBearer("Bearer", options =>
////{
////    options.RequireHttpsMetadata = false;
////    options.SaveToken = true;
////    options.TokenValidationParameters = tokenValidationParams;
////});


//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//});



//builder.Services.ConfigureSwaggerGen(options =>
//{
//    options.IncludeXmlComments($"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}{Assembly.GetExecutingAssembly().GetName().Name}.xml");
//    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "JWT Authorization header using the Bearer scheme.",

//    });
//    options.AddSecurityRequirement(new OpenApiSecurityRequirement
//   {
//       {
//           new OpenApiSecurityScheme
//           {
//               Reference = new OpenApiReference
//               {
//                   Type = ReferenceType.SecurityScheme,
//                   Id = "Bearer"
//               }
//           },
//           Array.Empty<string>()
//       }
//   });
//});


//builder.Services.AddAntiforgery(x =>
//{
//    x.HeaderName = "X-CSRF-TOKEN"; // Set the header name for CSRF token
//    x.Cookie.Name = "X-CSRF-TOKEN"; // Set the cookie name for CSRF token
//    x.SuppressXFrameOptionsHeader = false;
//    x.Cookie.HttpOnly = false;
//});

//var app = builder.Build();

////// Configure the HTTP request pipeline.
////if (app.Environment.IsDevelopment())
////{
////    app.UseSwagger();
////    app.UseSwaggerUI();
////}
//app.UseHttpsRedirection();
//app.UseCors();

//app.UseSwaggerForOcelotUI(opt =>
//{
//    opt.PathToSwaggerGenerator = "/swagger/docs";
//    opt.DownstreamSwaggerEndPointBasePath = "/swagger/docs";
//}, ui =>
//{
//    ui.RoutePrefix = "swagger";
//});


//app.UseMiddleware<InterceptionMiddleware>();
//app.UseMiddleware<ResterictAccessMiddleware>();
//app.UseMiddleware<TokenCheckerMiddleware>();


//app.UseAuthentication();
//app.UseAuthorization();

//app.UseRouting();




//app.MapControllers();
//await app.UseOcelot();
//app.Run();

using System.Text;
using System.Text.Json.Serialization;
using GateWay.Handler;
using GateWay.Middleware;
using JobSeeker.Shared.Kernel.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);



builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
//builder.Services.AddTransient<IgnoreSslValidationHandler>();

builder.Services.AddOcelot(builder.Configuration);//.AddDelegatingHandler<IgnoreSslValidationHandler>(true);
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });

builder.Services.AddSingleton<TokenValidationParameters>(sp =>
{
    return new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        ClockSkew = TimeSpan.FromMinutes(5) // optional: tolerance for clock drift
    };
});
// === CORS ===
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));


var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors();
//app.MapGet("/", () => Results.Content($@"
//    <html>

//        <head>
//            <title>JobSeeker API Gateway</title>
//            <style>
//                body {{ font-family: Arial; text-align: center; margin-top: 100px; background: #f0f2f5; }}
//                h1 {{ color: #1a73e8; }}
//                a {{ color: #1a73e8; font-size: 1.2em; }}
//            </style>
//        </head>
//        <body>
//            <h1>JobSeeker Microservices Gateway</h1>
//            <p><strong>Status:</strong> Running</p>
//            <p><strong>Time:</strong> {DateTime.Now:yyyy-MM-dd HH:mm:ss} CET</p>
//            <p><a href='/swagger'>Open Swagger UI (All 5 APIs)</a></p>
//            < hr >
//            < small > Account • Job • Profile • Advertisement • Assessment </ small >
//        </ body >
//    </ html >
//", "text / html"));
// Swagger UI
app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
}, ui => ui.RoutePrefix = "swagger");




// CUSTOM MIDDLEWARES (MUST BE BEFORE Ocelot)
app.UseMiddleware<InterceptionMiddleware>();
app.UseMiddleware<ResterictAccessMiddleware>();
app.UseMiddleware<TokenCheckerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();


await app.UseOcelot();
app.MapControllers();

app.Run();
