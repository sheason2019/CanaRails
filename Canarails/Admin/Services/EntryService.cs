using CanaRails.Controllers;
using CanaRails.Database;
using CanaRails.Exceptions;
using CanaRails.Transformer;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Services;

public class EntryService(
  CanaRailsContext context
)
{
  public async Task<Database.Entities.Entry> CreateEntry(EntryDTO dto)
  {
    using var transcation = context.Database.BeginTransaction();
    var count = await context.Entries.
      Where(e => e.App.ID.Equals(dto.AppId) && e.Name.Equals(dto.Name)).
      CountAsync();
    if (count > 0)
    {
      throw new HttpStandardException(400, "该应用下已存在同名应用入口");
    }

    var query = from apps in context.Apps
                where apps.ID.Equals(dto.AppId)
                select apps;
    var app = query.First();

    var entry = dto.ToEntity(app);
    context.Entries.Add(entry);
    await context.SaveChangesAsync();
    await transcation.CommitAsync();
    return entry;
  }

  public async Task<Database.Entities.Entry> FindByIDAsync(int id)
  {
    return await context.Entries.
      Include(e => e.App).
      Where(e => e.ID.Equals(id)).
      FirstAsync();
  }

  public async Task<int> CountAsync(int appID)
  {
    return await context.Entries.
      Where(e => e.App.ID.Equals(appID)).
      CountAsync();
  }

  public async Task<Database.Entities.Entry[]> ListAsync(int appID)
  {
    return await context.Entries.
      Where(e => e.App.ID.Equals(appID)).
      Include(e => e.App).
      ToArrayAsync();
  }
}
