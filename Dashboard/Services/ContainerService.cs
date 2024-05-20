using CanaRails.Adapters.IAdapter;
using CanaRails.Controllers.Container;
using CanaRails.Database;
using CanaRails.Database.Entities;

namespace CanaRails.Services;

public class ContainerService(
  CanaRailsContext context,
  IAdapter adapter
)
{
  public async Task<string[]> CreateCotnainer(ContainerDTO template, int replica)
  {
    // 创建 Container
    var imageQuery = from images in context.Images
                     where images.ID.Equals(template.ImageId)
                     select images;
    var image = imageQuery.First();

    var containerInfos = await adapter.Image.Apply(image, replica);

    // 获取 Entry 并提升版本
    var entryQuery = from entrys in context.Entries
                     where entrys.ID.Equals(template.EntryId)
                     select entrys;
    var entry = entryQuery.First();
    entry.Version++;

    // 创建 Container 集合
    var containers = containerInfos.Select(e => new Container
    {
      ContainerID = e.ContainerId,
      Port = template.Port,
      Version = entry.Version,
      Image = image,
      Entry = entry,
    });
    context.Containers.AddRange(containers);
    context.SaveChanges();
    return containers.Select(e => e.ContainerID).ToArray();
  }

  public async Task StopOldContainers(int entryId)
  {
    // 查询并停止旧容器
    var query = from containers in context.Containers
                join entries in context.Entries
                on containers.Entry.ID equals entries.ID
                where entries.ID.Equals(entryId)
                where containers.Version < entries.Version
                select containers;
    var records = query.ToArray();
    await adapter.Container.Stop(records);
  }
}