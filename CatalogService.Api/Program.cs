using CatalogService.Api;
using CatalogService.Application;
using CatalogService.Infrastructure;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataAccess();
builder.Services.AddApplicationLogic();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapProductEndpoints();

app.Run();
