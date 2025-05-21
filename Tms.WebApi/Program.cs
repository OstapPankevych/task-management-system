using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Tms.WebApi.DI;
using Tms.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var dbConnectionString = builder.Configuration["ConnectionStrings:TmsDbConnectionString"]!;

var rabbitOptions = builder.Configuration
     .GetSection("RabbitMq")
     .Get<RabbitMqOptions>()!;

var isDevelopment = builder.Environment.IsDevelopment();

builder.Services
    .Configure<JsonOptions>(options =>
    {
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddDataBase(dbConnectionString)
    .AddCap(isDevelopment, dbConnectionString, rabbitOptions)
    .AddServices()
    .AddExceptionHandler<ExceptionHandler>()
    .AddProblemDetails()
    .AddHealthChecks();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapTaskApis()
    .MapHealthChecks("health");

app.Run();