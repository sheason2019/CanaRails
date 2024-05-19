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
using CanaRails.Controllers.Container;
using CanaRails.Controllers.EntryMatcher;

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
        builder.Services.AddScoped<AppService>();
        builder.Services.AddScoped<AppMatcherService>();
        builder.Services.AddScoped<DockerService>();
        builder.Services.AddScoped<ImageService>();
        builder.Services.AddScoped<EntryService>();
        builder.Services.AddScoped<ContainerService>();

        // Add Controller
        builder.Services.AddScoped<IAppController, AppControllerImpl>();
        builder.Services.AddScoped<IAppMatcherController, AppMatcherControllerImpl>();
        builder.Services.AddScoped<IImageController, ImageControllerImpl>();
        builder.Services.AddScoped<IEntryController, EntryControllerImpl>();
        builder.Services.AddScoped<IEntryMatcherController, EntryMatcherControllerImpl>();
        builder.Services.AddScoped<IContainerController, ContainerControllerImpl>();

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

