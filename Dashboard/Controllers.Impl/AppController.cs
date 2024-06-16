using CanaRails.Adapter;
using CanaRails.Controllers.App;
using CanaRails.Database;
using CanaRails.Services;
using CanaRails.Transformer;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Controllers.Impl;

public class AppControllerImpl(
  AppService service,
  CanaRailsContext context,
  ContainerAdapter adapter
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
    var dto = query.Include(e => e.DefaultEntry).First().ToDTO();
    return Task.FromResult(dto);
  }

  public async Task<AppDTO> CreateAsync(AppDTO body)
  {
    var record = await service.CreateAppAsync(body);
    adapter.Apply();
    return record.ToDTO();
  }

  public Task CreateHostnameAsync(int id, Body body)
  {
    var queryApp = from apps in context.Apps
                   where apps.ID.Equals(id)
                   select apps;
    var app = queryApp.First();

    app.Hostnames.Add(body.Hostname);
    context.SaveChanges();

    adapter.Apply();

    return Task.CompletedTask;
  }

  public Task DeleteHostnameAsync(int id, string hostname)
  {
    var queryApp = from apps in context.Apps
                   where apps.ID.Equals(id)
                   select apps;
    var app = queryApp.First();

    app.Hostnames.RemoveAll(e => e == hostname);
    context.SaveChanges();

    adapter.Apply();

    return Task.CompletedTask;
  }

  public Task PutDefaultEntryAsync(int id, int entryId)
  {
    var queryApp = from apps in context.Apps
                   where apps.ID.Equals(id)
                   select apps;
    var app = queryApp.First();

    var queryEntry = from entries in context.Entries
                     where entries.ID.Equals(entryId)
                     select entries;
    var entry = queryEntry.First();

    app.DefaultEntry = entry;
    context.SaveChanges();

    adapter.Apply();

    return Task.CompletedTask;
  }

  public Task<int> DeleteAsync(int id)
  {
    var queryApp = from apps in context.Apps
                   where apps.ID.Equals(id)
                   select apps;
    var app = queryApp.Include(e => e.Entries).First();
    app.DefaultEntryId = null;
    context.SaveChanges();

    // PublishOrder
    context.PublishOrders.Where(e => e.Entry.App.ID.Equals(id)).ExecuteDelete();
    // Entry
    context.Entries.Where(e => e.App.ID.Equals(id)).ExecuteDelete();
    // Image
    context.Images.Where(e => e.App.ID.Equals(id)).ExecuteDelete();
    // App
    context.Apps.Where(e => e.ID.Equals(id)).ExecuteDelete();

    context.SaveChanges();

    adapter.Apply();

    return Task.FromResult(id);
  }
}
