using System.Diagnostics;
using System.Net;
using CanaRails.Database;
using CanaRails.Adapters.DockerAdapter;
using CanaRails.Interfaces;
using Microsoft.EntityFrameworkCore;
using Yarp.ReverseProxy.Forwarder;
using CanaRails.Adapters.DockerAdapter.Services;

namespace CanaRails.Ingress;

public class Program
{
  private static readonly HttpMessageInvoker httpClient = new(new SocketsHttpHandler
  {
    UseProxy = false,
    AllowAutoRedirect = false,
    AutomaticDecompression = DecompressionMethods.None,
    UseCookies = false,
    EnableMultipleHttp2Connections = true,
    ActivityHeadersPropagator = new ReverseProxyPropagator(
        DistributedContextPropagator.Current
      ),
    ConnectTimeout = TimeSpan.FromSeconds(15),
  });

  private static readonly ForwarderRequestConfig requestConfig = new()
  {
    ActivityTimeout = TimeSpan.FromSeconds(100)
  };

  private static readonly CanaRailsTransformer transformer = new();

  public static WebApplication CreateApplication()
  {

    var builder = WebApplication.CreateBuilder();

    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();

    builder.Services.AddDbContext<CanaRailsContext>();
    builder.Services.AddSingleton<CanaRailsContext>();
    builder.Services.AddSingleton<DockerService>();
    builder.Services.AddSingleton<IAdapter, DockerAdapter>();
    builder.Services.AddHttpForwarder();

    var app = builder.Build();

    app.UseRouting();

    app.Map("/{**url}", async (
      string? url,
      ILogger<Program> logger,
      HttpContext context,
      CanaRailsContext dbContext,
      IHttpForwarder forwarder,
      IAdapter adapter
    ) =>
    {
      // 解析 Host
      var host = context.Request.Host.Host;
      // 根据匹配器寻找命中的 APP
      var match = await dbContext.AppMatchers.
        Include(a => a.App).
        Where(a => a.Host.Equals(host)).
        FirstAsync();
      // TODO: 根据 Header 和 Cookie 匹配 Entry
      var entry = await dbContext.Entries.
        Where(e => e.App.ID.Equals(match.App.ID)).
        FirstAsync();
      // 寻找最新的 Container
      var container = await dbContext.Containers.
        Where(c => c.Entry.ID.Equals(entry.ID)).
        OrderByDescending(c => c.CreatedAt).
        FirstAsync();
      // 调用 Adapter 寻找容器 IP
      var ip = await adapter.GetContainerIP(container.ContainerID);

      var error = await forwarder.SendAsync(
        context,
        $"http://{ip}:{container.Port}",
        httpClient,
        requestConfig,
        transformer
      );
    });

    return app;
  }
  public static Task Main()
  {
    return CreateApplication().RunAsync("http://localhost:80");
  }

  public class CanaRailsTransformer : HttpTransformer { }
}

