using CanaRails.Adapters.DockerAdapter;
using CanaRails.Controllers;
using CanaRails.Controllers.Impl;
using CanaRails.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAdapter, DockerAdapter>();
builder.Services.AddSingleton<IAppController, AppControllerImpl>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
