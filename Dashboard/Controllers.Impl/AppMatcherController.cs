using CanaRails.Controllers.AppMatcher;
using CanaRails.Database;
using CanaRails.Services;
using CanaRails.Transformer;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Controllers.Impl;

public class AppMatcherControllerImpl(
  AppMatcherService appMatcherService,
  CanaRailsContext context
) : IAppMatcherController
{
  public Task<ICollection<AppMatcherDTO>> ListAsync(int appID)
  {
    var query = from matchers in context.AppMatchers
                where matchers.App.ID.Equals(appID)
                select matchers;
    ICollection<AppMatcherDTO> dtos = [.. query.
      Include(e => e.App).
      Select(r => r.ToDTO())];
    return Task.FromResult(dtos);
  }

  public Task<int> CountAsync(int appID)
  {
    return appMatcherService.CountAsync(appID);
  }

  public async Task<AppMatcherDTO> CreateAsync(int appID, AppMatcherDTO body)
  {
    var query = from apps in context.Apps
                where apps.ID.Equals(appID)
                select apps;
    var app = query.First();
    var appMatcher = await appMatcherService.CreateAsync(body, app);
    return appMatcher.ToDTO();
  }

  public Task<int> DeleteAsync(int appId, int matcherId)
  {
    context.AppMatchers.
      Where(e => e.App.ID.Equals(appId) && e.ID.Equals(matcherId)).
      ExecuteDelete();
    return Task.FromResult(matcherId);
  }
}
