
using CanaRails.Adapter;
using CanaRails.Controllers;
using CanaRails.Database;
using CanaRails.Database.Entities;
using CanaRails.Services;
using CanaRails.Transformer;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Controllers.Impl;

public class EntryControllerImpl(
  EntryService entryService,
  CanaRailsContext context,
  ContainerAdapter adapter
) : IEntryController
{
  public Task<int> CountAsync(int appID)
  {
    return entryService.CountAsync(appID);
  }

  public async Task<EntryDTO> CreateAsync(EntryDTO body)
  {
    var entry = await entryService.CreateEntry(body);

    adapter.Apply();

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

    adapter.Apply();

    return Task.CompletedTask;
  }

  public Task<int> DeleteAsync(int entryId)
  {
    var queryApp = from apps in context.Apps
                   join entries in context.Entries on apps.ID equals entries.App.ID
                   where entries.ID.Equals(entryId)
                   select apps;
    var app = queryApp.First();
    if (app.DefaultEntryId == entryId)
    {
      app.DefaultEntryId = null;
      context.SaveChanges();
    }

    // delete publish orders
    context.PublishOrders
      .Where(e => e.Entry.ID.Equals(entryId))
      .ExecuteDelete();
    // delete entry
    context.Entries
      .Where(e => e.ID.Equals(entryId))
      .ExecuteDelete();
    context.SaveChanges();

    adapter.Apply();

    return Task.FromResult(entryId);
  }

  public Task DeleteMatcherAsync(int id, string key)
  {
    var queryEntry = from entries in context.Entries
                     where entries.ID.Equals(id)
                     select entries;
    var entry = queryEntry.First();

    entry.EntryMatchers.RemoveAll(e => e.Key.Equals(key));
    context.SaveChanges();

    adapter.Apply();

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
