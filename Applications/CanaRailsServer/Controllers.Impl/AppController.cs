using CanaRails.Services;
using CanaRails.Transformer;

namespace CanaRails.Controllers.Impl;

public class AppControllerImpl(AppService service) : IAppController
{
  public async Task<AppDTO> CreateAsync(Body body)
  {
    var dto = body.Dto;
    var record = await service.CreateAppAsync(dto);
    return record.ToDTO();
  }

  public async Task<ICollection<AppDTO>> ListAsync()
  {
    var records = await service.ListAsync();
    return records.Select(record => record.ToDTO()).ToArray();
  }
}
