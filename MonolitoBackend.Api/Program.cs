using MonolitoBackend.Core.Services;
using MonolitoBackend.Infrastructure.Services;
using MonolitoBackend.Core.Interfaces;
using MonolitoBackend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MonolitoBackend.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MonolitoBackend.Core.Configuration;
using System.Security.Claims;
using MonolitoBackend.Infrastructure.Configurations;
using MonolitoBackend.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Configuração do JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Registro de serviços usando o Extension Method
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// Adicionando logging
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware de tratamento de exceções
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Habilitar CORS
app.UseCors("AllowFrontend");

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
