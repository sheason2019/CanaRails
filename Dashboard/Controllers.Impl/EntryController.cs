
using CanaRails.Controllers.Entry;
using CanaRails.Services;
using CanaRails.Transformer;

namespace CanaRails.Controllers.Impl;

public class EntryControllerImpl(
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
    return entry.ToDTO();
  }

  public async Task<EntryDTO> FindByIdAsync(int id)
  {
    var entry = await entryService.FindByIDAsync(id);
    return entry.ToDTO();
  }

  public async Task<ICollection<EntryDTO>> ListAsync(int appID)
  {
    var records = await entryService.ListAsync(appID);
    return records.Select(e => e.ToDTO()).ToArray();
  }

  public Task<int> UpdateAsync(int id, EntryDTO body)
  {
    throw new NotImplementedException();
  }
}
