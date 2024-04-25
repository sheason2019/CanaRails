using CanaRails.Adapters.DockerAdapter;
using CanaRails.Interfaces;
using CanaRails.Applications.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddSingleton<IAdapter, DockerAdapter>();

var app = builder.Build();

app.UseGrpcWeb();

app.MapGrpcService<AppService>().EnableGrpcWeb();

app.MapGet("/", () => "All gRPC service are supported by default in this example, and are callable from browser apps uisng the gRPC-Web protocal");

app.Run();
