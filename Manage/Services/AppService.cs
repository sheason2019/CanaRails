using CanaRails.Controllers.App;
using CanaRails.Database;
using CanaRails.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Services;

public class AppService(CanaRailsContext context)
{
  public async Task<Database.Entities.App> CreateAppAsync(AppDTO dto)
  {
    using var transaction = context.Database.BeginTransaction();
    // 校验 name 是否被占用
    var existRecords = await context.Apps.
      ToListAsync();
    var duplicateSet = new HashSet<string>();
    foreach (var record in existRecords)
    {
      if (record.Name == dto.Name)
      {
        duplicateSet.Add("App 名称已被使用");
      }
    }
    if (duplicateSet.Count > 0)
    {
      var errMsg = string.Join(",", duplicateSet);
      throw new HttpStandardException(StatusCodes.Status400BadRequest, errMsg);
    }

    var app = new Database.Entities.App
    {
      Name = dto.Name,
    };
    await context.Apps.AddAsync(app);
    await context.SaveChangesAsync();

    transaction.Commit();
    return app;
  }

  public async Task<Database.Entities.App[]> ListAsync()
  {
    return await context.Apps.ToArrayAsync();
  }

  public async Task<Database.Entities.App> FindByIDAsync(int id)
  {
    return await context.Apps.
      Where(record => record.ID.Equals(id)).
      FirstAsync();
  }

  public async Task<Database.Entities.Entry?> FindDefaultEntry(int id)
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