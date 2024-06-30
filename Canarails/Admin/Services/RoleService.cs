using CanaRails.Enum;
using Microsoft.AspNetCore.Identity;

namespace CanaRails.Services;

public class RoleService(
  RoleManager<IdentityRole> roleManager
)
{
  public async Task Setup()
  {
    await SetupAdmin();
  }

  public async Task SetupAdmin()
  {
    if (!await roleManager.RoleExistsAsync(Roles.Administrator))
    {
      await roleManager.CreateAsync(new IdentityRole { Name = Roles.Administrator });
    }
  }
}
