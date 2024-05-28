using CanaRails.ContainerAdapter.Models;
using CanaRails.ContainerAdapter.Utils;
using CanaRails.Database;
using CanaRails.Database.Entities;
using k8s;
using k8s.Models;

namespace CanaRails.ContainerAdapter.Services;

public class GatewayService(
  Kubernetes client
)
{
  // 检查 Gateway 是否存在，不存在则创建
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

    // 应用修改
    CustomObjectUtils.CreateOrPatch(
      client,
      gateway,
      group,
      version,
      plural,
      gateway.Metadata.Name
    );
  }
}
