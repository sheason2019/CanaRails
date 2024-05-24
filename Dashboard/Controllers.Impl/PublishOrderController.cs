using CanaRails.Controllers.PublishOrder;
using CanaRails.Database;
using CanaRails.Services;
using CanaRails.Transformer;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Controllers.Impl;

public class PublishOrderControllerImpl(
  CanaRailsContext context,
  PublishOrderService service
) : IPublishOrderController
{
  public Task<int> CreateAsync(PublishOrderDTO body)
  {
    // 创建工单
    var order = service.CreateOrder(body);

    // 异步执行工单
    // 这里的分离处理是为了方便后续在这一部分提供审批功能


    return Task.FromResult(order.ID);
  }

  public Task<PublishOrderDTO> GetByIdAsync(int id)
  {
    var query = from order in context.PublishOrders
      where order.ID.Equals(id)
      select order;
    var dto = query
      .Include(e => e.Image)
      .Include(e => e.Entry)
      .First()
      .ToDTO();
    return Task.FromResult(dto);
  }

  public Task<ICollection<PublishOrderDTO>> ListAsync(int entryId)
  {
    var query = from orders in context.PublishOrders
      where orders.Entry.ID.Equals(entryId)
      select orders;
    var dtos = query
      .Include(e => e.Image)
      .Include(e => e.Entry)
      .Select(e => e.ToDTO())
      .ToArray();
    return Task.FromResult<ICollection<PublishOrderDTO>>(dtos);
  }
}
