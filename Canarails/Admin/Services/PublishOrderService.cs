using CanaRails.Adapter;
using CanaRails.Controllers;
using CanaRails.Database;
using CanaRails.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Services;

public class PublishOrderService(
  CanaRailsContext context,
  ContainerAdapter adapter
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

  public void ApplyOrder(int orderId)
  {
    // 将 Entry 的 CurrentOrder 切换至当前 Order
    var queryOrder = from orders in context.PublishOrders
      where orders.ID.Equals(orderId)
      select orders;
    var order = queryOrder.Include(e => e.Entry).First();

    order.Status = PublishOrderStatus.Approval;
    context.SaveChanges();

    // 变更容器配置
    adapter.Apply();
  }
}