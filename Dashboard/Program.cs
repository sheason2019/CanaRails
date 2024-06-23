using k8s;
using CanaRails.Database;
using CanaRails.Controllers.Impl;
using CanaRails.Services;
using CanaRails.Exceptions;
using CanaRails.Controllers;
using CanaRails.Adapter;
using CanaRails.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CanaRails.Manage;

public class Program
{
  public static WebApplication CreateApplication()
  {
    var builder = WebApplication.CreateBuilder();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddDbContext<CanaRailsContext>();

    builder.Services.AddScoped(config =>
    {
      var clientConfig = EnvVariables.CANARAILS_CLIENT_CONFIG;
      if (clientConfig == "IN_CLUSTER")
      {
        return new AdapterConfiguration
        {
          Client = new Kubernetes(
            KubernetesClientConfiguration.InClusterConfig()
          )
        };
      }
      return new AdapterConfiguration
      {
        Client = new Kubernetes(
          KubernetesClientConfiguration.BuildConfigFromConfigFile(
            "/etc/rancher/k3s/k3s.yaml"
          )
        )
      };
    });
    builder.Services.AddScoped<ContainerAdapter>();

    // Authorization
    builder.Services.AddAuthorization();
    builder.Services.AddAuthentication()
    .AddCookie(IdentityConstants.ApplicationScheme);
    builder.Services.AddIdentityCore<IdentityUser>()
      .AddRoles<IdentityRole>()
      .AddUserManager<UserManager<IdentityUser>>()
      .AddSignInManager<SignInManager<IdentityUser>>()
      .AddEntityFrameworkStores<CanaRailsContext>();
    builder.Services.Configure<IdentityOptions>(options =>
    {
      options.Password.RequireLowercase = false;
      options.Password.RequireUppercase = false;
    });

    // Add Services
    builder.Services.AddScoped<AuthService>();
    builder.Services.AddScoped<AppService>();
    builder.Services.AddScoped<ImageService>();
    builder.Services.AddScoped<EntryService>();
    builder.Services.AddScoped<AdminService>();
    builder.Services.AddScoped<RoleService>();
    builder.Services.AddScoped<PublishOrderService>();

    // Add Controller
    builder.Services.AddScoped<IAppController, AppControllerImpl>();
    builder.Services.AddScoped<IImageController, ImageControllerImpl>();
    builder.Services.AddScoped<IEntryController, EntryControllerImpl>();
    builder.Services.AddScoped<IPublishOrderController, PublishOrderControllerImpl>();
    builder.Services.AddScoped<IAuthController, AuthControllerImpl>();

    builder.Services.AddControllers(options =>
    {
      options.Filters.Add<HttpStandardExceptionFilter>();
    });

    var app = builder.Build();

    // Database auto migrate
    using (var scope = app.Services.CreateScope())
    {
      // 服务启动时执行数据库迁移逻辑
      var db = scope.ServiceProvider.GetRequiredService<CanaRailsContext>();
      db.Database.Migrate();

      // 尝试连接至 Kubernetes Api 应用当前数据库中存储的状态
      // 此时如果没有权限对 Kubernetes 进行操作，则会抛出异常并中断服务
      var adapter = app.Services.GetRequiredService<ContainerAdapter>();
      adapter.Apply();

      // 初始化 role 信息
      var roleService = scope.ServiceProvider.GetRequiredService<RoleService>();
      roleService.Setup().Wait();

      // 初始化 admin 账户信息
      var adminService = scope.ServiceProvider.GetRequiredService<AdminService>();
      adminService.Setup().Wait();
    }

    app.MapControllers();

    // 添加对内置 SPA 页面的支持
    app.UseFileServer();
    app.MapFallbackToFile(Path.Join(app.Environment.WebRootPath, "index.html"));

    return app;
  }

  public static Task Main()
  {
    return CreateApplication().RunAsync("http://*:8080");
  }
}

