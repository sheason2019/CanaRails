using Admin.Apis;
using Admin.Domains.Auth.Actions;
using Admin.Domains.Auth.Services;
using Admin.Domains.Auth.Models;
using Admin.Infrastructure.Repository;
using Admin.Infrastructure.IDL;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;

namespace Admin.Domains.Auth.Application;

public static class Setup
{
  public static void AddAuthDomain(this IServiceCollection services)
  {
    // Authorization
    services.AddAuthorization();
    services.AddAuthentication()
      .AddCookie(IdentityConstants.ApplicationScheme);
    services.AddIdentityCore<IdentityUser>()
      .AddRoles<IdentityRole>()
      .AddUserManager<UserManager<IdentityUser>>()
      .AddSignInManager<SignInManager<IdentityUser>>()
      .AddEntityFrameworkStores<CanaRailsContext>();
    services.Configure<IdentityOptions>(options =>
    {
      options.Password.RequireLowercase = false;
      options.Password.RequireUppercase = false;
    });
    services.AddDataProtection()
      .PersistKeysToDbContext<CanaRailsContext>();

    services.AddScoped<AuthService>();
    services.AddScoped<LoginServie>();
    services.AddScoped<CurrentUser>();
    services.AddScoped<SetupAdmin>();
    services.AddScoped<SetupRole>();
    services.AddScoped<IAuthController, AuthControllerImpl>();
  }

  public static void SetupAuthDomain(this WebApplication app)
  {
    // Database auto migrate
    using var scope = app.Services.CreateScope();

    // 初始化 role 信息
    var setupRole = scope.ServiceProvider.GetRequiredService<SetupRole>();
    setupRole.Setup().Wait();

    // 初始化 admin 账户信息
    var setupAdmin = scope.ServiceProvider.GetRequiredService<SetupAdmin>();
    setupAdmin.Setup().Wait();
  }
}
