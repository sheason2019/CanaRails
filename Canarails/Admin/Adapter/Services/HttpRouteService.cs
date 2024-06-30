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
                   join entries in context.Entries on apps.ID equals entries.App.ID
                   join publishOrders in context.PublishOrders on entries.ID equals publishOrders.Entry.ID
                   where publishOrders.Status == PublishOrderStatus.Approval
                   select apps;
    var validApps = queryApp
      .Include(e => e.DefaultEntry)
      .Include(e => e.Entries)
      .ThenInclude(e => e.PublishOrders)
      .ToList();

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
      if (ApplyHttpRouteByApp(app))
      {
        deleteSet.Remove($"canarails-http-route-by-app-{app.ID}");
      }
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

  // 尝试应用 App
  // 返回值为 true 时表示应用成功，为 false 时表示应用失败
  // 应用失败时应当移除该 HTTPRoute
  public bool ApplyHttpRouteByApp(App app)
  {
    var routeName = $"canarails-http-route-by-app-{app.ID}";

    var validEntries = app.Entries
      .Where(e => e.EntryMatchers.Count > 0 || e.ID == app.DefaultEntryId)
      .ToList();

    // 当 App 无法访问时，尝试移除此 App 的 HTTPRoute
    if (app.Entries.Count == 0 || app.Hostnames.Count == 0 || validEntries.Count == 0)
    {
      return false;
    }

    // 根据 App 属性生成 HTTPRoute
    var route = new HttpRoute
    {
      ApiVersion = "gateway.networking.k8s.io/v1",
      Kind = "HTTPRoute",
      Metadata = new V1ObjectMeta
      {
        Name = routeName,
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
        Rules = validEntries.Select(entry => new HttpRouteRule
        {
          Matches = entry.ID == app.DefaultEntryId ? null : [
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
              RequestHeaderModifier = new HTTPHeaderFilter {
                Add = [
                  new HTTPHeader {
                    Name = "x-canarails-entry",
                    Value = entry.ID.ToString(),
                  },
                  new HTTPHeader {
                    Name = "x-canarails-app",
                    Value = app.ID.ToString(),
                  },
                ],
              },
            },
            new HttpRouteRuleFilter {
              Type = "ResponseHeaderModifier",
              ResponseHeaderModifier = new HTTPHeaderFilter {
                Add = [
                  new HTTPHeader {
                    Name = "x-canarails-entry",
                    Value = entry.ID.ToString(),
                  },
                  new HTTPHeader {
                    Name = "x-canarails-app",
                    Value = app.ID.ToString(),
                  },
                ],
              },
            }
          ],
          BackendRefs = [
            new HttpRouteRuleBackendRef {
              Name = $"canarails-service-by-entry-{entry.ID}",
              Port = entry
                .PublishOrders
                .Where(order => order.Status == PublishOrderStatus.Approval)
                .FirstOrDefault()
                ?.Port ?? 80,
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
    return true;
  }
}
