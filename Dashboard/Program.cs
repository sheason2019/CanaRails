using CanaRails.Database;
using CanaRails.Controllers.Impl;
using CanaRails.Services;
using CanaRails.Exceptions;
using CanaRails.Controllers.App;
using CanaRails.Controllers.Image;
using CanaRails.Controllers.Entry;
using CanaRails.Controllers.Container;
using CanaRails.Adapter;
using CanaRails.Controllers.PublishOrder;

namespace CanaRails.Manage;

public class Program
{
  public static WebApplication CreateApplication()
  {
    var builder = WebApplication.CreateBuilder();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddDbContext<CanaRailsContext>();

    builder.Services.AddScoped<ContainerAdapter>();

    // Add Services
    builder.Services.AddScoped<AppService>();
    builder.Services.AddScoped<ImageService>();
    builder.Services.AddScoped<EntryService>();
    builder.Services.AddScoped<ContainerService>();
    builder.Services.AddScoped<PublishOrderService>();

    // Add Controller
    builder.Services.AddScoped<IAppController, AppControllerImpl>();
    builder.Services.AddScoped<IImageController, ImageControllerImpl>();
    builder.Services.AddScoped<IEntryController, EntryControllerImpl>();
    builder.Services.AddScoped<IContainerController, ContainerControllerImpl>();
    builder.Services.AddScoped<IPublishOrderController, PublishOrderControllerImpl>();

    builder.Services.AddControllers(options => { options.Filters.Add<HttpStandardExceptionFilter>(); });

    var app = builder.Build();

    // 尝试连接至 Kubernetes Api 如果没有权限对 Kubernetes 进行操作，则抛出异常中断服务
    {
      var adapter = app.Services.GetRequiredService<ContainerAdapter>();
      adapter.Apply();
    }

    app.MapControllers();

    // 添加对内置 SPA 页面的支持
    app.UseFileServer();
    app.MapFallback(async (context) =>
    {
      context.Response.StatusCode = 200;
      await context.Response.SendFileAsync(
              Path.Join(app.Environment.WebRootPath, "index.html")
          );
    });

    return app;
  }

  public static Task Main()
  {
    return CreateApplication().RunAsync("http://localhost:8080");
  }
}

