using System.Security.Claims;
using Admin.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Admin.Domains.Auth.Models;

public class CurrentUserAccessor
{
  public IdentityUser? Value { get; private set; }

  public CurrentUserAccessor(
    CanaRailsContext context,
    UserManager<IdentityUser> userManager,
    IHttpContextAccessor httpContextAccessor
  )
  {
    // 尝试从 Cookie 中初始化当前用户信息
    var username = httpContextAccessor
      .HttpContext?
      .User
      .FindFirstValue(ClaimTypes.Name);
    if (username != null)
    {
      Task.Run(async () =>
      {
        Value = await userManager.FindByNameAsync(username);
      }).Wait();
    }
    if (Value != null) return;

    // 尝试从 Authorization Header 获取用户信息
    var authToken = httpContextAccessor
    ?.HttpContext
    ?.Request
    .Headers
    .Authorization
    .FirstOrDefault();
    if (authToken != null)
    {
      var uas = context.UserAccessSecrets
        .Where(e => e.Token == authToken)
        .Include(e => e.User)
        .FirstOrDefault();
      if (uas != null) {
        Value = uas.User;
      }
    }
  }
}
