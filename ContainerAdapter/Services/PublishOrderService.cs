using CanaRails.Database.Entities;
using k8s;

namespace CanaRails.ContainerAdapter.Services;

public class PublishOrderService(
  Kubernetes client,
  EntryService entryService
)
{
  public void Apply(PublishOrder order)
  {
    // 创建 Deployment

    // 更 PublishOrder 的更新应用到 Entry
    entryService.Apply(order.Entry);
  }
}
