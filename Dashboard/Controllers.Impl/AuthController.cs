using System.Security.Claims;
using CanaRails.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CanaRails.Controllers.Impl;

[Authorize]
public class AuthControllerImpl(
  SignInManager<IdentityUser> signInManager,
  UserManager<IdentityUser> userManager,
  IHttpContextAccessor httpContextAccessor
) : IAuthController
{
  public Task<AuthData> GetAuthDataAsync()
  {
    var username = httpContextAccessor
      .HttpContext?
      .User
      .FindFirstValue(ClaimTypes.Name)
      ?? throw new HttpStandardException(
        StatusCodes.Status401Unauthorized,
        "未登录"
      );

    var res = new AuthData
    {
      Username = username
    };

    return Task.FromResult(res);
  }

  [AllowAnonymous]
  public async Task LoginAsync(LoginReq body)
  {
    var user = await userManager.FindByNameAsync(body.Username)
      ?? throw new HttpStandardException(StatusCodes.Status404NotFound, "无法获取用户信息");

    var loginResult = await signInManager.PasswordSignInAsync(
      user,
      body.Password,
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