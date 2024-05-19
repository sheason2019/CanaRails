
using CanaRails.Controllers.Entry;
using CanaRails.Services;
using CanaRails.Transformer;

namespace CanaRails.Controllers.Impl;

public class EntryControllerImpl(
  AppService appService,
  EntryService entryService
) : IEntryController
{
  public Task<int> CountAsync(int appID)
  {
    return entryService.CountAsync(appID);
  }

  public async Task<EntryDTO> CreateAsync(EntryDTO body)
  {
    var entry = await entryService.CreateEntry(body);
    return entry.ToDTO().AddDeployInfo(entry);
  }

  public async Task<EntryDTO> FindByIDAsync(int id)
  {
    var entry = await entryService.FindByIDAsync(id);
    return entry.ToDTO().AddDeployInfo(entry);
  }

  public async Task<ICollection<EntryDTO>> ListAsync(int appID)
  {
    var records = await entryService.ListAsync(appID);
    return records.Select(e => e.ToDTO().AddDeployInfo(e)).ToArray();
  }

  public async Task<EntryDTO?> FindDefaultEntryAsync(int appID)
  {
    var record = await appService.FindDefaultEntry(appID);
    return record?.ToDTO().AddDeployInfo(record);
  }

  public Task PutDefaultEntryAsync(int appID, int entryID)
  {
    return Task.Run(() => appService.PutDefaultEntry(entryID));
  }

  public Task<int> UpdateAsync(int id, EntryDTO body)
  {
    throw new NotImplementedException();
  }
}
