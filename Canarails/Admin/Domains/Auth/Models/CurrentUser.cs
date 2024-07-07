using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Admin.Domains.Auth.Models;

public class CurrentUser
{
  public IdentityUser? Value { get; private set; }

  public CurrentUser(
    UserManager<IdentityUser> userManager,
    IHttpContextAccessor httpContextAccessor
  )
  {
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
  }
}
