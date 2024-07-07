using Admin.Domains.Core.Services;
using Admin.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Admin.Infrastructure.ProgramSetup;

public static class ApplicationSetup
{
  public static void InitialApplication(this WebApplication app)
  {
    // Database auto migrate
    using var scope = app.Services.CreateScope();
    // 服务启动时执行数据库迁移逻辑
    var db = scope.ServiceProvider.GetRequiredService<CanaRailsContext>();
    db.Database.Migrate();

    // 尝试连接至 Kubernetes Api 应用当前数据库中存储的状态
    // 此时如果没有权限对 Kubernetes 进行操作，则会抛出异常并中断服务
    var gatewayService = app.Services.GetRequiredService<GatewayService>();
    gatewayService.Update();
  }

  // 初始化对 SPA 的支持
  public static void UseSinglePageApplication(this WebApplication app)
  {
    // 添加对内置 SPA 页面的支持
    app.UseFileServer();
    app.MapFallback(async (context) =>
    {
      context.Response.StatusCode = 200;
      await context.Response.SendFileAsync(
        Path.Join(app.Environment.WebRootPath, "index.html")
      );
    });
  }
}