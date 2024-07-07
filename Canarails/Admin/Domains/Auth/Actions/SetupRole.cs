using Admin.Domains.Auth.Constants;
using Microsoft.AspNetCore.Identity;

namespace Admin.Domains.Auth.Actions;

public class SetupRole(
  RoleManager<IdentityRole> roleManager
)
{
  public async Task Setup()
  {
    await SetupAdmin();
  }

  private async Task SetupAdmin()
  {
    if (!await roleManager.RoleExistsAsync(Roles.Administrator))
    {
      await roleManager.CreateAsync(new IdentityRole { Name = Roles.Administrator });
    }
  }
}
