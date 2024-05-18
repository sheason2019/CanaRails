using CanaRails.Controllers.App;
using CanaRails.Database;
using CanaRails.Database.Entities;
using CanaRails.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Services;

public class AppService(CanaRailsContext context)
{
  public async Task<App> CreateAppAsync(AppDTO dto)
  {
    using var transaction = context.Database.BeginTransaction();
    // 校验 App 名称是否已被占用
    var query = from apps in context.Apps
                where apps.Name.Equals(dto.Name)
                select apps;
    var count = await query.CountAsync();
    if (count > 0)
    {
      throw new HttpStandardException(
        StatusCodes.Status400BadRequest,
        "App 名称已被使用"
      );
    }

    var value = await context.Apps.AddAsync(new App
    {
      Name = dto.Name,
      Description = dto.Description,
    });
    await context.SaveChangesAsync();

    await transaction.CommitAsync();
    return value.Entity;
  }

  public async Task<App[]> ListAsync()
  {
    return await context.Apps.ToArrayAsync();
  }

  public async Task<Entry?> FindDefaultEntry(int id)
  {
    return await context.Entries.
      Where(entry => entry.App.ID.Equals(id) && entry.Default.Equals(true)).
      Include(entry => entry.Containers).
      FirstOrDefaultAsync();
  }

  public void PutDefaultEntry(int entryID)
  {
    using var transcation = context.Database.BeginTransaction();
    var query = from app in context.Apps
                join entry in context.Entries
                on app.ID equals entry.App.ID
                where entry.ID.Equals(entryID)
                select entry;
    foreach (var entry in query.ToArray())
    {
      entry.Default = entry.ID == entryID;
    }
    context.SaveChanges();
    transcation.Commit();
  }
}