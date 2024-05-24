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
}