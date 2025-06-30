using CatalogService.Api;
using CatalogService.Api.Dtos.Validators;
using CatalogService.Api.Exceptions;
using CatalogService.Application;
using CatalogService.Infrastructure;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDockerDataAccess(builder.Configuration);

builder.Services.AddApplicationLogic();

builder.Services.MapApiValidators();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddLogger(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
    app.Services.CreateScope().ApplyMigrations();

app.UseExceptionHandler();

app.MapProductEndpoints();

app.Run();

public partial class Program { }
