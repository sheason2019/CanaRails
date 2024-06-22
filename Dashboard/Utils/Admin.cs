using CanaRails.Database;
using CanaRails.Database.Entities;

namespace CanaRails.Utils;

public class AdminUtils(CanaRailsContext context)
{
  // 初始化管理员账户
  public void SetupAdmin(string adminPassword)
  {
    using var transcation = context.Database.BeginTransaction();

    var queryAdmin = from users in context.Users
                     where users.Username.Equals("admin")
                     select users;
    var admin = queryAdmin.FirstOrDefault();
    var (salt, passwordHash) = AuthUtils.CreatePasswordWithHash(adminPassword);

    if (admin != null)
    {
      admin.PasswordHash = passwordHash;
      admin.PasswordSalt = salt;
    }
    else
    {
      admin = new User
      {
        Username = "admin",
        PasswordHash = passwordHash,
        PasswordSalt = salt,
      };
      context.Users.Add(admin);
    }

    context.SaveChanges();

    transcation.Commit();
  }
}