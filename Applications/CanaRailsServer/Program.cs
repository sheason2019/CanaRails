using CanaRails.Database;
using CanaRails.Adapters.DockerAdapter;
using CanaRails.Controllers.Impl;
using CanaRails.Interfaces;
using CanaRails.Services;
using CanaRails.Exceptions;
using CanaRails.Controllers.App;
using CanaRails.Adapters.DockerAdapter.Services;

namespace CanaRails.Server;

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
    });

    var app = builder.Build();

    app.MapControllers();

    return app;
  }
  public static void Main(string[] args)
  {
    CreateApplication().Run();
  }
}

