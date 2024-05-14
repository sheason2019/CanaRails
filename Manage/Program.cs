using CanaRails.Database;
using CanaRails.Adapters.DockerAdapter;
using CanaRails.Controllers.Impl;
using CanaRails.Services;
using CanaRails.Exceptions;
using CanaRails.Controllers.App;
using CanaRails.Adapters.DockerAdapter.Services;
using CanaRails.Controllers.Image;
using CanaRails.Controllers.Entry;
using CanaRails.Controllers.AppMatcher;
using CanaRails.Adapters.IAdapter;

namespace CanaRails.Manage;

public class Program
{
    public static WebApplication CreateApplication()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddDbContext<CanaRailsContext>();

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

        builder.Services.AddControllers(options => { options.Filters.Add<HttpStandardExceptionFilter>(); });

        var app = builder.Build();

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

