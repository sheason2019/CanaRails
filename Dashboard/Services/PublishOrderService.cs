using CanaRails.Adapters.IAdapter;
using CanaRails.Controllers.PublishOrder;
using CanaRails.Database;
using CanaRails.Database.Entities;

namespace CanaRails.Services;

public class PublishOrderService(
  CanaRailsContext context,
  IAdapter adapter
)
{
  public PublishOrder CreateOrder(PublishOrderDTO dto)
  {
    var queryEntry = from entries in context.Entries
      where entries.ID.Equals(dto.EntryId)
      select entries;
    var entry = queryEntry.First();
    
    var queryImage = from images in context.Images
      where images.ID.Equals(dto.ImageId)
      select images;
    var image = queryImage.First();

    var record = new PublishOrder
    {
      Entry = entry,
      Image = image,
      Port = dto.Port,
      Replica = dto.Replica,
    };
    context.PublishOrders.Add(record);
    context.SaveChanges();

    return record;
  }

  public async Task ApplyOrder(PublishOrder order)
  {
    // 为当前 Order 创建容器集合
    var containers = await adapter.Order.Start(order);
    

    // 将 Entry 的 CurrentOrder 切换至当前 Order

    // 停止其他 Order 创建的容器
  }
}