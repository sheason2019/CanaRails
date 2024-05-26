using CanaRails.Database.Entities;
using k8s;
using k8s.Models;

namespace CanaRails.ContainerAdapter.Services;

public class EntryService(Kubernetes client, GatewayService gatewayService)
{
  // 当 Entry 状态发生变化时调用此函数更新 Service
  public void Apply(Entry entry)
  {
    var name = GetEntryServiceName(entry);
    var svr = new V1Service
    {
      Metadata = {
        Name = name,
      },
      Spec = {
        Selector = {
          {
            "canarails-publish-order-id",
            entry.CurrentPublishOrder?.ID.ToString() ?? "null"
          },
        },
        Ports = [
          new V1ServicePort{
            Name = "canarails-order-export-port",
            Protocol = "TCP",
            Port = entry.CurrentPublishOrder?.Port ?? 80,
            TargetPort = entry.CurrentPublishOrder?.Port ?? 80,
          },
        ],
      },
    };

    // Patch Service
    client.CoreV1.PatchNamespacedService(
      new V1Patch(svr, V1Patch.PatchType.MergePatch),
      name,
      Constant.Namespace
    );

    gatewayService.Apply();
  }

  // 删除 Entry 时调用此函数
  public V1Service Delete(Entry entry)
  {
    // Delete Service
    var name = GetEntryServiceName(entry);

    return client.CoreV1.DeleteNamespacedService(
      name,
      Constant.Namespace
    );
  }

  public V1Service GetService(Entry entry)
  {
    return client.CoreV1.ReadNamespacedService(
      GetEntryServiceName(entry),
      Constant.Namespace
    );
  }

  private static string GetEntryServiceName(Entry entry)
  {
    return $"canarails-application-service-by-entry-{entry.ID}";
  }
}
