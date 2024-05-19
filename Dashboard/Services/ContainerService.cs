using CanaRails.Adapters.IAdapter;
using CanaRails.Controllers.Container;
using CanaRails.Database;
using CanaRails.Database.Entities;
using CanaRails.Transformer;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Services;

public class ContainerService(
  CanaRailsContext context,
  IAdapter adapter
)
{
  public async Task<Container> PutContainerAsync(
    ContainerDTO dto,
    Image image,
    Entry entry
  )
  {
    // // 先创建新的容器
    // var cid = await adapter.CreateContainer(image);
    // var container = dto.ToEntity(image, entry);

    // // 将 Container 添加到数据库
    // container.CreatedAt = DateTime.Now;
    // container.ContainerID = cid;
    // context.Containers.Add(container);
    // await context.SaveChangesAsync();

    // // 停止所有 image 中所有的 Container
    // await StopContainersAsync(entry, container.ID);
    // return container;
    throw new NotImplementedException();
  }

  public async Task StopContainersAsync(Entry entry, int? excludeID)
  {
    // var stopTasks = context.Containers.
    //   Where(c => c.Entry.ID.Equals(entry.ID) && !c.ID.Equals(excludeID)).
    //   Select(c => adapter.StopContainer(c.ContainerID));
    // await Task.WhenAll(stopTasks);
    throw new NotImplementedException();
  }

  public async Task<Container[]> ListContainerAsync(int entryID)
  {
    return await context.Containers.
      Include(c => c.Image).
      Include(c => c.Entry).
      Where(c => c.Entry.ID.Equals(entryID)).
      ToArrayAsync();
  }
}