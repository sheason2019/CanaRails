using Admin.Domains.Auth.Services;
using Admin.Domains.Auth.Models;
using Admin.Infrastructure.Exceptions;
using Admin.Infrastructure.IDL;

namespace Admin.Apis;

public class AuthControllerImpl(
  CurrentUser currentUser,
  LoginServie loginService
) : IAuthController
{
  public Task<AuthData> GetAuthDataAsync()
  {
    var user = currentUser.Value
      ?? throw new HttpStandardException(
        StatusCodes.Status401Unauthorized,
        "未登录"
      ); ;

    return Task.FromResult(new AuthData { Username = user?.UserName });
  }

  public async Task LoginAsync(LoginReq body)
  {
    await loginService.LoginAsync(body.Username, body.Password);
  }

  public async Task LogoutAsync()
  {
    await loginService.LogoutAsync();
  }
}