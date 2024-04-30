using System.Diagnostics;
using System.Net;
using CanaRails.Database;
using Yarp.ReverseProxy.Forwarder;

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

    builder.Services.AddDbContext<CanaRailsContext>();
    builder.Services.AddSingleton<CanaRailsContext>();
    builder.Services.AddHttpForwarder();

    var app = builder.Build();

    app.UseRouting();

    app.Map("/{**url}", async (
      string? url,
      HttpContext context,
      IHttpForwarder forwarder
    ) =>
    {
      var error = await forwarder.SendAsync(
        context,
        "http://localhost:8080",
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

