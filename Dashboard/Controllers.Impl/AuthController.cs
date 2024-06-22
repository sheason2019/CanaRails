using CanaRails.Controllers;

namespace CanaRails.Controllers.Impl;

public class AuthControllerImpl : IAuthController
{
  public Task<AuthData> GetAuthDataAsync()
  {
    throw new NotImplementedException();
  }

  public Task<LoginRes> LoginAsync(LoginReq body)
  {
    throw new NotImplementedException();
  }
}