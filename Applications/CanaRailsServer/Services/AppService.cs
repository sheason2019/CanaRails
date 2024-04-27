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
    // 校验 Host 和 name 是否被占用
    var existRecords = await context.Apps.
      Where(record => record.Host.Equals(dto.Host)).
      ToListAsync();
    var duplicateSet = new HashSet<string>();
    foreach (var record in existRecords)
    {
      if (record.Host == dto.Host)
      {
        duplicateSet.Add("Host 已存在");
      }
      if (record.Name == dto.Name)
      {
        duplicateSet.Add("Name 已存在");
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
      Host = dto.Host,
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
}