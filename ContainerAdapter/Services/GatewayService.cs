using System.Text.Json;
using CanaRails.ContainerAdapter.Models;
using k8s;
using k8s.Models;

namespace CanaRails.ContainerAdapter.Services;

public class GatewayService(Kubernetes client)
{
  private readonly string gatewayName = "canarails-gateway";
  private readonly string ns = "default";

  // 根据当前数据库记录更新主干 Ingress 路由逻辑
  public void UpdateGateway()
  {
    // 更新 gateway
    ReplaceGateway();
    // 更新 Http Router
    ReplaceHttpRoute();
  }

  public void ReplaceHttpRoute()
  {
    var group = "gateway.networking.k8s.io";
    var version = "v1";
    var plural = "httproutes";

    var route = new HttpRoute
    {
      ApiVersion = "gateway.networking.k8s.io/v1",
      Kind = "HTTPRoute",
      Metadata = new V1ObjectMeta
      {
        Name = "canarails-route",
        NamespaceProperty = ns,
      },
      Spec = new HttpRouteSpec
      {
        ParentRefs = [
          new HttpRouteParentRef {
            Name = gatewayName,
          },
        ],
        Hostnames = ["httpbin.example.com"],
        Rules = [
          new HttpRouteRule {
            Matches = [
              new HttpRouteRuleMatch {
                Path = new HttpRouteRuleMatchPath{
                  Type = "PathPrefix",
                  Value = "/get"
                },
              },
            ],
            BackendRefs = [
              new HttpRouteRuleBackendRef {
                Name = "httpbin",
                Port = 8000,
              },
            ],
          }
        ],
      },
    };

    client.PatchNamespacedCustomObject(
      new V1Patch(route, V1Patch.PatchType.MergePatch),
      group,
      version,
      ns,
      plural,
      route.Metadata.Name
    );
  }

  public void ReplaceGateway()
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
        Name = gatewayName,
        NamespaceProperty = ns,
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

    client.CustomObjects.PatchNamespacedCustomObject(
      new V1Patch(gateway, V1Patch.PatchType.MergePatch),
      group,
      version,
      ns,
      plural,
      gateway.Metadata.Name
    );
  }
}