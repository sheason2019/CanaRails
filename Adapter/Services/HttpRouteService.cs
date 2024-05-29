using System.Text.RegularExpressions;
using CanaRails.Adapter.Models;
using CanaRails.Adapter.Utils;
using CanaRails.Database;
using CanaRails.Database.Entities;
using k8s;
using k8s.Models;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Adapter.Services;

public class HttpRouteService(
  Kubernetes client,
  CanaRailsContext context
)
{
  private readonly string group = "gateway.networking.k8s.io";
  private readonly string version = "v1";
  private readonly string plural = "httproutes";

  public void ApplyHttpRoute()
  {
    // 查询当前应生效的 App
    var queryApp = from apps in context.Apps
                   where apps.Entries
                    .Select(e => e.CurrentPublishOrder != null)
                    .Count() > 0
                   select apps;
    var validApps = queryApp
      .Include(e => e.Hostnames)
      .Include(e => e.Entries)
      .ToArray();

    // 查询集群中已生效的 HttpRoute
    var routes = client.ListNamespacedCustomObject<CustomResourceList<HttpRoute>>(
      group,
      version,
      Constant.Namespace,
      plural
    );

    // 计算应当删除的 App，这里执行一次正则匹配，避免误删
    var deleteSet = new HashSet<string>() { };
    foreach (var route in routes.Items ?? [])
    {
      var appId = ParseIdUtils.ParseAppIdByHttpRouteName(route.Metadata.Name);
      if (appId != null) deleteSet.Add(route.Metadata.Name);
    }

    foreach (var app in validApps)
    {
      deleteSet.Remove($"canarails-http-route-by-app-{app.ID}");
      ApplyHttpRouteByApp(app);
    }

    foreach (var name in deleteSet)
    {
      client.DeleteNamespacedCustomObject(
        group,
        version,
        Constant.Namespace,
        plural,
        name
      );
    }
  }
  public void ApplyHttpRouteByApp(App app)
  {
    // 每个 App 生成一个 HTTPRoute
    // 将 App 注册的 Hostname 填入 Host 字段
    // 将 Entry 构成的小流量泳道记录至 Rules 字段
    var route = new HttpRoute
    {
      ApiVersion = "gateway.networking.k8s.io/v1",
      Kind = "HTTPRoute",
      Metadata = new V1ObjectMeta
      {
        Name = $"canarails-http-route-by-app-{app.ID}",
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
        Hostnames = app.Hostnames,
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
              Name = $"canarails-service-by-entry-{entry.CurrentPublishOrder!.ID}",
              Port = entry.CurrentPublishOrder!.Port,
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
