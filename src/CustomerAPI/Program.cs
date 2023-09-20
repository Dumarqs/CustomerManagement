using Carter;
using CustomerAPI.Middlewares;
using Domain.Helpers;
using Infra.CrossCutting.IoC;
using Microsoft.OpenApi.Models;
using System.Reflection;

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
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
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

public partial class Program { }