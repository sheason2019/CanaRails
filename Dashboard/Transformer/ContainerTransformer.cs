using CanaRails.Controllers.Container;
using CanaRails.Database.Entities;

namespace CanaRails.Transformer;

public static class ContainerTransformer
{
  public static ContainerDTO ToDTO(this Container container)
  {
    return new ContainerDTO
    {
      Id = container.ID,
      ContainerId = container.ContainerID,
    };
  }
}
