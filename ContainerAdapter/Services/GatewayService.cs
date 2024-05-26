using CanaRails.ContainerAdapter.Models;
using k8s;
using k8s.Models;

namespace CanaRails.ContainerAdapter.Services;

public class GatewayService(
  Kubernetes client
)
{
  // 根据数据库内容全量更新 K8s Gateway API
  public void Apply()
  {
    // 更新 gateway
    ApplyGateway();
    // 更新 Http Router
    ApplyHttpRoute();
  }

  public void ApplyHttpRoute()
  {
    var group = "gateway.networking.k8s.io";
    var version = "v1";
    var plural = "httproutes";

    // TODO: 根据 App 和 Entry 动态生成 HTTPRoute
    var route = new HttpRoute
    {
      ApiVersion = "gateway.networking.k8s.io/v1",
      Kind = "HTTPRoute",
      Metadata = new V1ObjectMeta
      {
        Name = Constant.HttpRouteName,
        NamespaceProperty = Constant.Namespace,
      },
      Spec = new HttpRouteSpec
      {
        ParentRefs = [
          new HttpRouteParentRef {
            Name = Constant.GatewayName,
            Namespace = Constant.Namespace,
          },
        ],
        // TODO: 这里收集 App 所有的 Matcher
        Hostnames = ["localhost"],
        // TODO: 这里为每个 Entry 创建一个 Rule
        Rules = [
          new HttpRouteRule {
            Matches = [
              new HttpRouteRuleMatch
              {
                Path = new HTTPPathMatch{
                  Type = "PathPrefix",
                  Value = "/get"
                },
                Headers = [
                  new HTTPHeaderMatch {
                    Type = "Exact",
                    Name = "x-httpbin",
                    Value = "1",
                  }
                ],
              },
            ],
            Filters = [
              new HttpRouteRuleFilter {
                Type = "RequestHeaderModifier",
                RequestHeaderModifier = new HttpRouteRuleRequestHeaderModifier {
                  Add = [
                    new HttpRouteRuleRequestHeaderModifierAdd {
                      Name = "x-canarails-entry",
                      Value = "1",
                    },
                    new HttpRouteRuleRequestHeaderModifierAdd {
                      Name = "x-canarails-app",
                      Value = "1",
                    },
                  ],
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

    // 尝试替换，如果不存在则创建
    // TODO: 这里的逻辑明显存在问题，应当在 StatusCode == 404 的时候再执行 Create 逻辑
    try
    {
      client.GetNamespacedCustomObject(
        group,
        version,
        Constant.Namespace,
        plural,
        route.Metadata.Name
      );
      client.PatchNamespacedCustomObject(
        new V1Patch(route, V1Patch.PatchType.MergePatch),
        group,
        version,
        Constant.Namespace,
        plural,
        route.Metadata.Name
      );
    }
    catch
    {
      client.CreateNamespacedCustomObject(
        route,
        group,
        version,
        Constant.Namespace,
        plural
      );
    }

  }

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