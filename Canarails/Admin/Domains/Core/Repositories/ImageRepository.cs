using Admin.Domains.Core.Models.Entities;
using Admin.Infrastructure.Repository;
using Admin.Infrastructure.IDL;
using Microsoft.EntityFrameworkCore;

namespace Admin.Domains.Core.Repositories;

public class ImageRepository(CanaRailsContext context)
{
  public Image Create(ImageDTO dto)
  {
    // 创建实体
    var query = from apps in context.Apps
                where apps.ID.Equals(dto.AppId)
                select apps;
    var app = query.First();

    var image = new Image
    {
      ID = dto.Id,
      ImageName = dto.ImageName,
      App = app,
      CreatedAt = DateTimeOffset.FromUnixTimeMilliseconds(dto.CreatedAt).DateTime,
    };
    image.CreatedAt = DateTime.UtcNow;

    // 成功后将实体写入数据库
    context.Images.Add(image);
    context.SaveChanges();

    return image;
  }


  public List<Image> ListImageByAppId(int appId)
  {
    var query = from images in context.Images
                join apps in context.Apps on images.App.ID equals apps.ID
                where apps.ID.Equals(appId)
                orderby images.CreatedAt descending
                select images;
    return [.. query.Include(e => e.App)];
  }

  public Image FindById(int id)
  {
    var query = from images in context.Images
                where images.ID.Equals(id)
                select images;
    return query.First();
  }

  public int Count(int appId)
  {
    var query = from images in context.Images
                join apps in context.Apps on images.App.ID equals apps.ID
                where apps.ID.Equals(appId)
                select images;
    return query.Count();
  }
}
