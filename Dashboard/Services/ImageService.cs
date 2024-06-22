using CanaRails.Adapter;
using CanaRails.Controllers;
using CanaRails.Database;
using CanaRails.Database.Entities;
using CanaRails.Transformer;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Services;

public class ImageService(
  CanaRailsContext context
)
{
  public async Task<Image> CreateImageAsync(ImageDTO dto)
  {
    // 创建实体
    var query = from apps in context.Apps
                where apps.ID.Equals(dto.AppId)
                select apps;
    var app = query.First();

    Image image = dto.ToEntity(app);
    image.CreatedAt = DateTime.UtcNow;

    // 成功后将实体写入数据库
    await context.Images.AddAsync(image);
    await context.SaveChangesAsync();

    return image;
  }

  public Task<List<Image>> ListImageByAppIDAsync(int appID)
  {
    return Task.FromResult<List<Image>>(
      [.. context.Images.
        Include(record => record.App).
        Where(record => record.App.ID.Equals(appID)).
        OrderByDescending(e => e.CreatedAt)
      ]
    );
  }

  public Task<Image> FindByIDAsync(int id)
  {
    return context.Images.Where(i => i.ID.Equals(id)).FirstAsync();
  }

  public Task<int> CountAsync(int appID)
  {
    return context.Images.
      Where(i => i.App.ID.Equals(appID)).
      CountAsync();
  }
}