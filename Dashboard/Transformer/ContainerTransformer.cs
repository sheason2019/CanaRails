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
      ImageId = container.Image.ID,
      EntryId = container.Entry.ID,
      Port = container.Port,
    };
  }

  public static Container ToEntity(
    this ContainerDTO dto,
    Image image,
    Entry entry
  )
  {
    return new Container
    {
      ID = dto.Id,
      ContainerID = dto.ContainerId,
      Version = dto.Version,
      Image = image,
      Entry = entry,
      Port = dto.Port,
    };
  }
}