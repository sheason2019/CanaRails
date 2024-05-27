using CanaRails.ContainerAdapter.Models;
using CanaRails.ContainerAdapter.Utils;
using CanaRails.Database.Entities;
using k8s;
using k8s.Models;

namespace CanaRails.ContainerAdapter.Services;

public class HttpRouteService(Kubernetes client)
{
  
  public void ApplyHttpRouteByApp(App app)
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

    CustomObjectUtils.CreateOrPatch(
      client,
      route,
      group,
      version,
      plural,
      route.Metadata.Name
    );
  }
}
