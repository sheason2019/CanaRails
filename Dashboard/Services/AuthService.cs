using CanaRails.Database;
using CanaRails.Database.Entities;
using CanaRails.Utils;

namespace CanaRails.Services;

public class AuthService(CanaRailsContext context)
{
  public User Login(string username, string password)
  {
    var queryUser = from users in context.Users
                    where users.Username.Equals(username)
                    select users;
    var user = queryUser.First();

    var passwordHash = AuthUtils.GetPasswordHash(password, user.PasswordSalt);
    if (!passwordHash.Equals(user.PasswordHash))
    {
      throw new Exception("用户名或密码错误");
    }

    return user;
  }
}