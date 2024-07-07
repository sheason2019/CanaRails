using Admin.Domains.Core.Models.Entities;
using Admin.Infrastructure.Exceptions;
using Admin.Infrastructure.Repository;
using Admin.Infrastructure.IDL;
using Microsoft.EntityFrameworkCore;

namespace Admin.Domains.Core.Repositories;

public class EntryRepository(CanaRailsContext context)
{
  public Entry Create(EntryDTO dto)
  {
    using var transcation = context.Database.BeginTransaction();

    var queryCount = from entries in context.Entries
                     where entries.App.ID.Equals(dto.AppId) && entries.Name.Equals(dto.Name)
                     select entries;
    var count = queryCount.Count();
    if (count > 0)
    {
      throw new HttpStandardException(400, "该应用下已存在同名应用入口");
    }

    var query = from apps in context.Apps
                where apps.ID.Equals(dto.AppId)
                select apps;
    var app = query.First();

    var entry = new Entry
    {
      ID = dto.Id,
      Name = dto.Name,
      Description = dto.Description,
      EntryMatchers = dto
        .Matchers
        .Select(e => new EntryMatcher
        {
          Key = e.Key,
          Value = e.Value,
        })
        .ToList(),
      App = app,
    };
    context.Entries.Add(entry);
    context.SaveChanges();
    transcation.Commit();

    return entry;
  }

  public void Delete(int id)
  {
    var entry = FindById(id);

    using var transcation = context.Database.BeginTransaction();
    var queryApp = from apps in context.Apps
                   join entries in context.Entries on apps.ID equals entries.App.ID
                   where entries.ID.Equals(entry.ID)
                   select apps;
    var app = queryApp.First();

    if (app.DefaultEntryId == entry.ID)
    {
      app.DefaultEntryId = null;
      context.SaveChanges();
    }

    // delete entry versions
    context.EntryVersions
      .Where(e => e.Entry.ID.Equals(entry.ID))
      .ExecuteDelete();
    // delete entry
    context.Entries
      .Where(e => e.ID.Equals(entry.ID))
      .ExecuteDelete();
    context.SaveChanges();

    transcation.Commit();
  }

  public Entry FindById(int id)
  {
    var query = from entries in context.Entries
                where entries.ID.Equals(id)
                select entries;
    return query.Include(e => e.App).First();
  }

  public Entry[] ListByAppId(int appId)
  {
    var queryRecords = from entries in context.Entries
                       join apps in context.Apps on entries.App.ID equals apps.ID
                       where apps.ID == appId
                       select entries;
    return [.. queryRecords.Include(e => e.App)];
  }

  public int CountByAppId(int appId)
  {
    var queryRecords = from entries in context.Entries
                       join apps in context.Apps on entries.App.ID equals apps.ID
                       where apps.ID == appId
                       select entries;
    return queryRecords.Count();
  }
}
