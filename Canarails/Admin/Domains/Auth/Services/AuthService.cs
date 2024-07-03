using Admin.Domains.Auth.Models;
using Admin.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Admin.Domains.Auth.Services;

public class AuthService(
  UserManager<IdentityUser> userManager,
  CurrentUser currentUser
)
{
  public async Task RequireRole(string role)
  {
    if (currentUser.Value != null)
    {
      if (await userManager.IsInRoleAsync(currentUser.Value, role))
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