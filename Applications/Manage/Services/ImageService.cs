using CanaRails.Controllers.Image;
using CanaRails.Database;
using CanaRails.Database.Entities;
using CanaRails.Interfaces;
using CanaRails.Transformer;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Services;

public class ImageService(
  CanaRailsContext context,
  AppService appService,
  IAdapter adapter
)
{
  public async Task<Image> CreateImageAsync(ImageDTO dto)
  {
    // 创建实体
    var app = await appService.FindByIDAsync(dto.AppID);
    var image = dto.ToEntity(app);
    // 尝试拉取镜像
    await adapter.PullImage(image);
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
        Where(record => record.App.ID.Equals(appID))
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