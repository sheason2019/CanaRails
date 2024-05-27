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
  // 根据数据库内容全量更新 K8s Gateway API
  public void Apply()
  {
    var app = new App
    {
      Name = "Mock App",
    };
    // 更新 gateway
    ApplyGateway();
    // 更新 Http Router
    ApplyHttpRoute(app);
  }

  public void ApplyHttpRoute(App app)
  {
    var group = "gateway.networking.k8s.io";
    var version = "v1";
    var plural = "httproutes";

    // 每个 App 生成一个 HTTPRoute，并将 App 注册的 Hostname 填入 Host 字段
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
        Hostnames = app.AppMatchers.Select(e => e.Host).ToList(),
        // 每个 Entry 创建一个 Rule
        Rules = app.Entries.Select(entry => new HttpRouteRule
        {
          Matches = [
            new HttpRouteRuleMatch
            {
              Headers = entry.EntryMatchers.Select(em => new HTTPHeaderMatch
              {
                Type = "Exact",
                Name = em.Key,
                Value = em.Value,
              }).ToList(),
            },
          ],
          Filters = [
            new HttpRouteRuleFilter {
              Type = "RequestHeaderModifier",
              RequestHeaderModifier = new HttpRouteRuleRequestHeaderModifier {
                Add = [
                  new HttpRouteRuleRequestHeaderModifierAdd {
                    Name = "x-canarails-entry",
                    Value = entry.ID.ToString(),
                  },
                  new HttpRouteRuleRequestHeaderModifierAdd {
                    Name = "x-canarails-app",
                    Value = app.ID.ToString(),
                  },
                ],
              },
            },
          ],
          BackendRefs = [
            new HttpRouteRuleBackendRef {
              Name = $"canarails-entry-service-{entry.CurrentPublishOrder?.ID}",
              Port = entry.CurrentPublishOrder?.Port,
            },
          ],
        }).ToList(),
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

  public void ApplyService()
  {
    // 遍历 Entry，设置 Service
  }

  public void ApplyDeployment()
  {
    // 遍历 Entry 中的 CurrentPublishOrder，设置 Deployment
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