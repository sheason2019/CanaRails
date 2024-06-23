
using CanaRails.Adapter;
using CanaRails.Database;
using CanaRails.Database.Entities;
using CanaRails.Enum;
using CanaRails.Services;
using CanaRails.Transformer;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Controllers.Impl;

public class EntryControllerImpl(
  EntryService entryService,
  CanaRailsContext context,
  ContainerAdapter adapter,
  AuthService authService
) : IEntryController
{
  public Task<int> CountAsync(int appID)
  {
    return entryService.CountAsync(appID);
  }

  public async Task<EntryDTO> CreateAsync(EntryDTO body)
  {
    await authService.RequireRole(Roles.Administrator);

    var entry = await entryService.CreateEntry(body);
    adapter.Apply();

    return entry.ToDTO();
  }

  public async Task CreateMatcherAsync(int id, EntryMatcherDTO body)
  {
    await authService.RequireRole(Roles.Administrator);

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
  }

  public async Task<int> DeleteAsync(int entryId)
  {
    await authService.RequireRole(Roles.Administrator);

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

    return entryId;
  }

  public async Task DeleteMatcherAsync(int id, string key)
  {
    await authService.RequireRole(Roles.Administrator);

    var queryEntry = from entries in context.Entries
                     where entries.ID.Equals(id)
                     select entries;
    var entry = queryEntry.First();

    entry.EntryMatchers.RemoveAll(e => e.Key.Equals(key));
    context.SaveChanges();

    adapter.Apply();
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
