using Admin.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Admin.Domains.Auth.Services;

public class LoginServie
(
  SignInManager<IdentityUser> signInManager,
  UserManager<IdentityUser> userManager
)
{
  public async Task LoginAsync(string username, string password)
  {
    var user = await userManager.FindByNameAsync(username)
      ?? throw new HttpStandardException(StatusCodes.Status404NotFound, "无法获取用户信息");

    var loginResult = await signInManager.PasswordSignInAsync(
      user,
      password,
      false,
      false
    );
    if (!loginResult.Succeeded)
    {
      throw new HttpStandardException(
        StatusCodes.Status400BadRequest,
        "登陆失败，用户名或密码错误"
      );
    }
  }

  public async Task LogoutAsync()
  {
    await signInManager.SignOutAsync();
  }
}