using System.Security.Claims;
using CanaRails.Enum;
using CanaRails.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace CanaRails.Services;

public class AuthService(
  IHttpContextAccessor httpContextAccessor,
  UserManager<IdentityUser> userManager
)
{
  public async Task RequireRole(string role)
  {
    var username = httpContextAccessor
      .HttpContext?
      .User
      .FindFirstValue(ClaimTypes.Name);
    if (username != null)
    {
      var user = await userManager.FindByNameAsync(username);
      if (user != null && await userManager.IsInRoleAsync(user, Roles.Administrator))
      {
        return;
      }
    }

    throw new HttpStandardException(
      StatusCodes.Status403Forbidden,
      "权限不足"
    );
  }
}