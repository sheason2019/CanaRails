using CanaRails.Enum;
using CanaRails.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace CanaRails.Services;

public class AdminService(
  UserManager<IdentityUser> userManager
)
{
  public async Task Setup()
  {
    await SetupUser();
    await SetupRole();
  }

  public async Task SetupUser()
  {
    var adminPassword = EnvVariables.CANARAILS_ADMIN_PSWD ?? "";
    var admin = await userManager.FindByNameAsync("admin");
    if (admin == null)
    {
      var createResult = await userManager.CreateAsync(
        new IdentityUser("admin"),
        adminPassword
      );
      if (!createResult.Succeeded)
      {
        var errMessage = Strings.Join(
          createResult.Errors.Select(e => e.Description).ToArray(),
          "\n"
        );
        throw new Exception($"create admin error:\n{errMessage}");
      }
    }
    else
    {
      var deleteResult = await userManager.RemovePasswordAsync(admin);
      var addResult = await userManager.AddPasswordAsync(admin, adminPassword);
      if (!deleteResult.Succeeded || !addResult.Succeeded)
      {
        var errMessage = Strings.Join(
          deleteResult.Errors
            .Concat(addResult.Errors)
            .Select(e => e.Description)
            .ToArray(),
          "\n"
        );
        throw new Exception($"update admin password error:{errMessage}");
      }
    }
  }

  public async Task SetupRole()
  {
    var admin = (await userManager.FindByNameAsync("admin"))!;
    if (!await userManager.IsInRoleAsync(admin, Roles.Administrator))
    {
      await userManager.AddToRoleAsync(admin, Roles.Administrator);
    }
  }
}