using CatalogService.Api;
using CatalogService.Api.Dtos.Validators;
using CatalogService.Application;
using CatalogService.Infrastructure;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddApplicationLogic();
builder.Services.MapApiValidators();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddLogger(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapProductEndpoints();

app.Run();
