using Microsoft.AspNetCore.Mvc;
using Notes.Application;
using Notes.Infrastructure;
using Notes.Persistance;
using Notes.Persistance.Configuration;
using Notes.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);
builder.Services.AddInfrastructure(configuration);
builder.Services.AddProblemDetails();

// Configure Keycloak authentication
// builder.Services.AddKeycloakAuthentication(configuration);

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();
