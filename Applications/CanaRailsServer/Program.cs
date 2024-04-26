using CanaRails.Adapters.DockerAdapter;
using CanaRails.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAdapter, DockerAdapter>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
