using Admin.Domains.Core.Models.Entities;
using Admin.Infrastructure.Exceptions;
using Admin.Infrastructure.Repository;
using Admin.Infrastructure.IDL;
using Microsoft.EntityFrameworkCore;

namespace Admin.Domains.Core.Repositories;

public class AppRepository(CanaRailsContext context)
{
  public App Create(AppDTO dto)
  {
    using var transaction = context.Database.BeginTransaction();
    // 校验 App 名称是否已被占用
    var query = from apps in context.Apps
                where apps.Name.Equals(dto.Name)
                select apps;
    var count = query.Count();
    if (count > 0)
    {
      throw new HttpStandardException(
        StatusCodes.Status400BadRequest,
        "App 名称已被使用"
      );
    }

    var value = context.Apps.Add(new App
    {
      Name = dto.Name,
      Description = dto.Description,
    });
    context.SaveChanges();

    transaction.Commit();
    return value.Entity;
  }

  public void Delete(int id)
  {
    var app = FindById(id);
    app.DefaultEntryId = null;
    context.SaveChanges();

    // PublishOrder
    context.EntryVersions.Where(e => e.Entry.App.ID.Equals(id)).ExecuteDelete();
    // Entry
    context.Entries.Where(e => e.App.ID.Equals(id)).ExecuteDelete();
    // Image
    context.Images.Where(e => e.App.ID.Equals(id)).ExecuteDelete();
    // App
    context.Apps.Where(e => e.ID.Equals(id)).ExecuteDelete();

    context.SaveChanges();
  }

  public App FindById(int id)
  {
    var queryApp = from apps in context.Apps
                   where apps.ID.Equals(id)
                   select apps;
    return queryApp.First();
  }
}
