using CanaRails.Database;
using CanaRails.Services;

namespace CanaRails.Utils;

public class AdminUtils(CanaRailsContext context, AuthService authService)
{
  // 初始化管理员账户
  public void SetupAdmin(string adminPassword)
  {
    using var transcation = context.Database.BeginTransaction();

    var queryAdmin = from users in context.Users
                     where users.Username.Equals("admin")
                     select users;
    var admin = queryAdmin.First();

    var (salt, passwordHash) = authService.HashPassword(adminPassword);
    admin.PasswordHash = passwordHash;
    admin.PasswordSalt = salt;

    context.SaveChanges();

    transcation.Commit();
  }
}