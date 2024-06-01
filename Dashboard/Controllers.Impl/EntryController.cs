
using CanaRails.Controllers.Entry;
using CanaRails.Database;
using CanaRails.Database.Entities;
using CanaRails.Services;
using CanaRails.Transformer;

namespace CanaRails.Controllers.Impl;

public class EntryControllerImpl(
  EntryService entryService,
  CanaRailsContext context
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

  public Task CreateMatcherAsync(int id, EntryMatcherDTO body)
  {
    var queryEntry = from entries in context.Entries
                     where entries.ID.Equals(id)
                     select entries;
    var entry = queryEntry.First();

    entry.EntryMatchers.Add(new EntryMatcher
    {
      Key = body.Key,
      Value = body.Value,
    });

    context.SaveChanges();
    return Task.CompletedTask;
  }

  public Task DeleteMatcherAsync(int id, string key)
  {
    var queryEntry = from entries in context.Entries
                     where entries.ID.Equals(id)
                     select entries;
    var entry = queryEntry.First();

    entry.EntryMatchers.RemoveAll(e => e.Key.Equals(key));
    context.SaveChanges();

    return Task.CompletedTask;
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
