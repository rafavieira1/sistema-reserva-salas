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

// Registro de serviços usando o Extension Method
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
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

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
