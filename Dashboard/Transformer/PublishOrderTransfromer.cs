using CanaRails.Controllers;
using CanaRails.Database.Entities;

namespace CanaRails.Transformer;

public static class PublishOrderTransfromer
{
  public static PublishOrderDTO ToDTO(this PublishOrder order)
  {
    return new PublishOrderDTO
    {
      Id = order.ID,
      ImageId = order.Image.ID,
      EntryId = order.Entry.ID,
      Port = order.Port,
      Replica = order.Replica,
      CreatedAt = ((DateTimeOffset)order.CreatedAt).ToUnixTimeMilliseconds(),
      Status = order.Status.ToString(),
    };
  }
}