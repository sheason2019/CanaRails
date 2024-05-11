
using CanaRails.Adapters.IAdapter;
using CanaRails.Services;
using CanaRails.Transformer;

namespace CanaRails.Controllers.Entry;

public class EntryControllerImpl(
  AppService appService,
  EntryService entryService,
  ImageService imageService,
  ContainerService containerService,
  IAdapter adapter
) : IEntryController
{
  public Task<int> CountAsync(int appID)
  {
    return entryService.CountAsync(appID);
  }

  public async Task<EntryDTO> CreateAsync(Body body)
  {
    var entry = await entryService.CreateEntry(body.Dto);
    return await entry.ToDTO().AddDeployInfo(entry, adapter);
  }

  public async Task<EntryDTO> FindByIDAsync(int id)
  {
    var entry = await entryService.FindByIDAsync(id);
    return await entry.ToDTO().AddDeployInfo(entry, adapter);
  }

  public async Task<ICollection<EntryDTO>> ListAsync(int appID)
  {
    var records = await entryService.ListAsync(appID);
    return await Task.WhenAll(records.Select(
      e => e.ToDTO().AddDeployInfo(e, adapter)
    ));
  }

  public async Task<ICollection<ContainerDTO>> ListContainerAsync(int id)
  {
    var containers = await containerService.ListContainerAsync(id);
    var dtos = containers.Select(c => c.ToDTO()).ToList();

    var states = await adapter.GetContainerState(
      dtos.Select(d => d.ContainerID).ToArray()
    );
    for (var i = 0; i < dtos.Count; i++)
    {
      dtos[i].State = states[i];
    }

    return dtos;
  }

  public async Task<ContainerDTO> PutContainerAsync(int id, Body2 body)
  {
    var entry = await entryService.FindByIDAsync(body.Dto.EntryID);
    var image = await imageService.FindByIDAsync(body.Dto.ImageID);
    var container = await containerService.PutContainerAsync(
      body.Dto,
      image,
      entry
    );
    return container.ToDTO();
  }

  public async Task<EntryDTO?> FindDefaultEntryAsync(int appID)
  {
    var record = await appService.FindDefaultEntry(appID);
    if (record == null)
    {
      return null;
    }

    return await record.ToDTO().AddDeployInfo(record, adapter);
  }

  public Task PutDefaultEntryAsync(int appID, int entryID)
  {
    return Task.Run(() =>
    {
      appService.PutDefaultEntry(entryID);
    });
  }
}