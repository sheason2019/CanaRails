using CanaRails.Controllers.App;
using CanaRails.Database;
using CanaRails.Services;
using CanaRails.Transformer;

namespace CanaRails.Controllers.Impl;

public class AppControllerImpl(
  AppService service,
  CanaRailsContext context
) : IAppController
{
  public async Task<ICollection<AppDTO>> ListAsync()
  {
    var records = await service.ListAsync();
    return records.Select(record => record.ToDTO()).ToArray();
  }

  public Task<AppDTO> FindByIDAsync(int id)
  {
    var query = from app in context.Apps
                where app.ID.Equals(id)
                select app;
    var dto = query.First().ToDTO();
    return Task.FromResult(dto);
  }

  public async Task<AppDTO> CreateAsync(AppDTO body)
  {
    var record = await service.CreateAppAsync(body);
    return record.ToDTO();
  }
}
