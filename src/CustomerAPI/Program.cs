using Carter;
using CustomerAPI.Middlewares;
using Domain.Helpers;
using Infra.CrossCutting.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Description = "Customer API",
        Version = "v1",
        Title = "Customer API",
    });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Put **ONLY** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddCarter();

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
    cfg.RegisterServicesFromAssemblies(typeof(DomainDependencyInjection).Assembly);
});

builder.Services.AddHealthChecks();
builder.Services.AddLog();
builder.Services.AddRepository();
builder.Services.AddUnitOfWork();

OptionsConfigurationServiceCollectionExtensions.Configure<ConnectionStrings>(builder.Services, builder.Configuration.GetSection("ConnectionStrings"));
JwtConfigurations jwtOptions = builder.Configuration.GetSection("JwtParameters").Get<JwtConfigurations>();
builder.Services.AddSingleton(jwtOptions);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidIssuer = jwtOptions.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapHealthChecks("/check");

app.MapCarter();

app.Run();

namespace CustomerAPI
{
    public partial class Program { }
}