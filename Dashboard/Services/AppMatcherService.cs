using CanaRails.Controllers.AppMatcher;
using CanaRails.Database;
using CanaRails.Database.Entities;
using CanaRails.Transformer;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Services;

public class AppMatcherService(CanaRailsContext context)
{
  public async Task<AppMatcher> CreateAsync(AppMatcherDTO dto, App app)
  {
    var appMatcher = dto.ToEntity(app);
    context.AppMatchers.Add(appMatcher);
    await context.SaveChangesAsync();
    return appMatcher;
  }

  public async Task<int> CountAsync(int appID)
  {
    return await context.AppMatchers.
      Where(r => r.App.ID.Equals(appID)).
      CountAsync();
  }
}
