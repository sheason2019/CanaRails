
using CanaRails.Controllers.Container;
using CanaRails.Database;
using CanaRails.Services;
using CanaRails.Transformer;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Controllers.Impl;

public class ContainerControllerImpl(
  ContainerService containerService,
  CanaRailsContext context
) : IContainerController
{
  public Task<ICollection<ContainerDTO>> ListAsync(int entryId)
  {
    var query = from containers in context.Containers
                where containers.Entry.ID.Equals(entryId)
                select containers;
    var records = query.
      Include(e => e.Entry).
      Include(e => e.Image).
      ToArray();
    return Task.FromResult<ICollection<ContainerDTO>>(
      records.Select(e => e.ToDTO()).ToArray()
    );
  }

  public async Task PutContainersAsync(Body body)
  {
    // 创建新的 Container
    await containerService.CreateCotnainer(body.Container, body.Replica);
    // 停止旧容器
    await containerService.StopOldContainers(body.Container.EntryId);
  }
}
