using CanaRails.Controllers.AppMatcher;
using CanaRails.Database;
using CanaRails.Database.Entities;
using CanaRails.Transformer;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Services;

public class AppMatcherService(CanaRailsContext context)
{
  public Task<List<AppMatcher>> ListAsync(int appID)
  {
    return context.AppMatchers.
      Include(r => r.App).
      Where(r => r.App.ID.Equals(appID)).
      ToListAsync();
  }
  public async Task<AppMatcher> Create(AppMatcherDTO dto, App app)
  {
    var appMatcher = dto.ToEntity(app);
    context.AppMatchers.Add(appMatcher);
    await context.SaveChangesAsync();
    return appMatcher;
  }
}
