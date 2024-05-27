using CanaRails.ContainerAdapter.Models;
using CanaRails.Database;
using CanaRails.Database.Entities;
using k8s;
using k8s.Models;

namespace CanaRails.ContainerAdapter.Services;

public class GatewayService(
  Kubernetes client
)
{
  public void ApplyGateway()
  {
    var group = "gateway.networking.k8s.io";
    var version = "v1";
    var plural = "gateways";

    var gateway = new Gateway
    {
      ApiVersion = "gateway.networking.k8s.io/v1",
      Kind = "Gateway",
      Metadata = new V1ObjectMeta
      {
        Name = Constant.GatewayName,
        NamespaceProperty = Constant.Namespace,
      },
      Spec = new GatewaySpec
      {
        GatewayClassName = "istio",
        Listeners = [
          new GatewayListener {
            Name = "http",
            Protocol = "HTTP",
            Port = 80,
            AllowedRoutes = new GatewayAllowedRoutes {
              Namespaces = new GatewayAllowedRoutesNamespaces {
                From = "All",
              },
            },
          },
        ],
      },
    };

    // 尝试替换，若不存在则创建
    // TODO: 这里或许可以抽象成一个泛型方法
    try
    {
      client.GetNamespacedCustomObject(
        group,
        version,
        Constant.Namespace,
        plural,
        gateway.Metadata.Name
      );
      client.PatchNamespacedCustomObject(
        new V1Patch(gateway, V1Patch.PatchType.MergePatch),
        group,
        version,
        Constant.Namespace,
        plural,
        gateway.Metadata.Name
      );
    }
    catch
    {
      client.CreateNamespacedCustomObject(
        gateway,
        group,
        version,
        Constant.Namespace,
        plural
      );
    }
  }
}
