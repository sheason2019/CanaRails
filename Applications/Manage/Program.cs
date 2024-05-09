using CanaRails.Database;
using CanaRails.Adapters.DockerAdapter;
using CanaRails.Controllers.Impl;
using CanaRails.Interfaces;
using CanaRails.Services;
using CanaRails.Exceptions;
using CanaRails.Controllers.App;
using CanaRails.Adapters.DockerAdapter.Services;
using CanaRails.Controllers.Image;
using CanaRails.Controllers.Entry;
using CanaRails.Controllers.AppMatcher;

namespace CanaRails.Manage;

public class Program
{
  public static WebApplication CreateApplication()
  {
    var builder = WebApplication.CreateBuilder();

    builder.Services.AddDbContext<CanaRailsContext>();
    builder.Services.AddSingleton<CanaRailsContext>();

    // Add Container adapter
    builder.Services.AddSingleton<IAdapter, DockerAdapter>();

    // Add Services
    builder.Services.AddSingleton<AppService>();
    builder.Services.AddSingleton<AppMatcherService>();
    builder.Services.AddSingleton<DockerService>();
    builder.Services.AddSingleton<ImageService>();
    builder.Services.AddSingleton<EntryService>();
    builder.Services.AddSingleton<ContainerService>();

    // Add Controller
    builder.Services.AddSingleton<IAppController, AppControllerImpl>();
    builder.Services.AddSingleton<IAppMatcherController, AppMatcherControllerImpl>();
    builder.Services.AddSingleton<IImageController, ImageControllerImpl>();
    builder.Services.AddSingleton<IEntryController, EntryControllerImpl>();

    /**
    * 由于应用需要集成到 Integration App 中发布
    * 这里需要使用 AddApplicationPart 将 Controller 扫描到 builder 中
    */
    builder.Services.AddControllers(options =>
    {
      options.Filters.Add<HttpStandardExceptionFilter>();
    })
    .AddApplicationPart(typeof(AppController).Assembly)
    .AddApplicationPart(typeof(AppMatcherController).Assembly)
    .AddApplicationPart(typeof(ImageController).Assembly)
    .AddApplicationPart(typeof(EntryController).Assembly)
    ;

    var app = builder.Build();

    app.MapControllers();

    return app;
  }
  public static Task Main()
  {
    return CreateApplication().RunAsync("http://localhost:8080");
  }
}

