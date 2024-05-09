using CanaRails.Controllers.AppMatcher;
using CanaRails.Services;
using CanaRails.Transformer;

namespace CanaRails.Controllers.Impl;

public class AppMatcherControllerImpl(
  AppMatcherService appMatcherService,
  AppService appService
) : IAppMatcherController
{
  public async Task<AppMatcherDTO> CreateAsync(int appID, Body body)
  {
    var app = await appService.FindByIDAsync(appID);
    var appMatcher = await appMatcherService.CreateAsync(body.Dto, app);
    return appMatcher.ToDTO();
  }

  public async Task<ICollection<AppMatcherDTO>> ListAsync(int appID)
  {
    var records = await appMatcherService.ListAsync(appID);
    return records.Select(r => r.ToDTO()).ToList();
  }

  public Task<int> CountAsync(int appID)
  {
    return appMatcherService.CountAsync(appID);
  }
}
