using CanaRails.Controllers;
using CanaRails.Services;

namespace CanaRails.Controllers.Impl;

public class AuthControllerImpl(AuthService authService) : IAuthController
{
  public Task<AuthData> GetAuthDataAsync()
  {
    throw new NotImplementedException();
  }

  public Task<LoginRes> LoginAsync(LoginReq body)
  {
    var user = authService.Login(body.Username, body.Password);
    // TODO: 添加创建 JWT 的方法
    var resp = new LoginRes
    {
      Jwt = ""
    };

    return Task.FromResult(resp);
  }
}