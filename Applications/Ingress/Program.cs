using System.Diagnostics;
using System.Net;
using CanaRails.Database;
using CanaRails.Adapters.DockerAdapter;
using Microsoft.EntityFrameworkCore;
using Yarp.ReverseProxy.Forwarder;
using CanaRails.Adapters.DockerAdapter.Services;
using CanaRails.Adapters.IAdapter;

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
    builder.Services.AddSingleton<MatchService>();
    builder.Services.AddSingleton<IAdapter, DockerAdapter>();
    builder.Services.AddHttpForwarder();

    var app = builder.Build();

    app.UseRouting();

    app.Map("/{**url}", async (
      string? url,
      HttpContext context,
      CanaRailsContext dbContext,
      MatchService matchService,
      IHttpForwarder forwarder,
      IAdapter adapter
    ) =>
    {
      var app = await matchService.MatchApp(context);
      if (app == null)
      {
        return Results.NotFound("App not found");
      }

      context.Response.Headers["X-Canarails-App-Name"] = app.Name;
      context.Response.Headers["X-Canarails-App-ID"] = app.ID.ToString();

      var entry = await matchService.MatchEntry(context, app);
      if (entry == null)
      {
        return Results.NotFound("Entry not found");
      }

      context.Response.Headers["X-Canarails-Entry-Name"] = entry.Name;
      context.Response.Headers["X-Canarails-Entry-ID"] = entry.ID.ToString();

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

      return Results.Empty;
    });

    return app;
  }
  public static Task Main()
  {
    return CreateApplication().RunAsync("http://localhost:80");
  }

  public class CanaRailsTransformer : HttpTransformer { }
}

