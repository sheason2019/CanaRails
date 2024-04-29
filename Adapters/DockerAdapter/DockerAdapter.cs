using CanaRails.Adapters.DockerAdapter.Services;
using CanaRails.Database.Entities;
using CanaRails.Interfaces;

namespace CanaRails.Adapters.DockerAdapter;

public class DockerAdapter(DockerService service) : IAdapter
{
  public async Task ApplyEntry(Entry entry)
  {
    await DeleteEntry(entry);
    // 创建新的容器
    if (entry.CurrentImage != null)
    {
      await service.CreateImageAsync(entry.CurrentImage);
      entry.CurrentContainerID = await service.CreateContainerAsync(
        entry.CurrentImage
      );
    }
  }
  public async Task DeleteEntry(Entry entry)
  {
    // 清理入口指向的容器
    if (entry.CurrentContainerID != null) {
      await service.StopContainerAsync(entry.CurrentContainerID);
      await service.RemoveContainerAsync(entry.CurrentContainerID);
      entry.CurrentContainerID = null;
    }
  }
}
