using CanaRails.Database;
using CanaRails.Adapters.DockerAdapter;
using CanaRails.Controllers.Impl;
using CanaRails.Interfaces;
using CanaRails.Services;
using CanaRails.Exceptions;
using CanaRails.Controllers.App;
using CanaRails.Adapters.DockerAdapter.Services;

namespace CanaRails.Manage;

public class Program
{
  public static WebApplication CreateApplication()
  {
    var builder = WebApplication.CreateBuilder();

    builder.Services.AddDbContext<CanaRailsContext>();
    builder.Services.AddSingleton<CanaRailsContext>();
    builder.Services.AddSingleton<AppService>();
    builder.Services.AddSingleton<DockerService>();
    builder.Services.AddSingleton<IAdapter, DockerAdapter>();
    builder.Services.AddSingleton<IAppController, AppControllerImpl>();
    builder.Services.AddControllers(options =>
    {
      options.Filters.Add<HttpStandardExceptionFilter>();
    })
    .AddApplicationPart(typeof(AppController).Assembly);

    var app = builder.Build();

    app.MapControllers();

    return app;
  }
  public static Task Main()
  {
    return CreateApplication().RunAsync("http://localhost:8080");
  }
}

