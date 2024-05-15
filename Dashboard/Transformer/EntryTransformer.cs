using CanaRails.Adapters.IAdapter;
using CanaRails.Controllers.Entry;
using CanaRails.Database.Entities;

namespace CanaRails.Transformer;

public static class EntryTransformer
{
  public static EntryDTO ToDTO(this Entry entry)
  {
    return new EntryDTO
    {
      Id = entry.ID,
      Name = entry.Name,
      Description = entry.Description,
      AppID = entry.App.ID,
    };
  }

  public static Entry ToEntity(this EntryDTO dto, App app)
  {
    return new Entry
    {
      ID = dto.Id,
      Name = dto.Name,
      Description = dto.Description,
      App = app,
    };
  }

  public static async Task<EntryDTO> AddDeployInfo(
    this EntryDTO dto,
    Entry record,
    IAdapter adapter
  )
  {
    var container = record.Containers.
      OrderByDescending(c => c.CreatedAt).
      FirstOrDefault();
    if (container == null) return dto;

    var state = await adapter.GetContainerState([container.ContainerID]);
    dto.DeployedAt = ((DateTimeOffset)container.CreatedAt).
      ToUnixTimeMilliseconds();
    dto.State = (await adapter.GetContainerState([container.ContainerID]))[0];
    return dto;
  }
}
