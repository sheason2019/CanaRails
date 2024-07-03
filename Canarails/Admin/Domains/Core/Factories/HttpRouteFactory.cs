using Admin.Domains.Core.Models;
using Admin.Domains.Core.Models.Entities;
using Admin.Domains.Core.Models.Gateway;
using Admin.Domains.Core.Models.Mappings;
using k8s.Models;

namespace Admin.Domains.Core.Factories;

public class HttpRouteFactory(
  ResourceNameFactory resourceNameFactory,
  HttpRouteRuleFactory httpRouteRuleFactory,
  CanaRailsClient client
)
{
  public HttpRoute Create(App app, EntryMapping[] entryMappings)
  {
    return new HttpRoute
    {
      ApiVersion = "gateway.networking.k8s.io/v1",
      Kind = "HTTPRoute",
      Metadata = new V1ObjectMeta
      {
        Name = resourceNameFactory.GetName(app.ID),
        NamespaceProperty = client.Configuration.Namespace,
        Labels = new Dictionary<string, string>{
          {"app-id", app.ID.ToString()}
        },
      },
      Spec = new HttpRouteSpec
      {
        ParentRefs = [
          new HttpRouteParentRef {
            Name = client.Configuration.GatewayName,
            Namespace = client.Configuration.Namespace,
          },
        ],
        Hostnames = app.Hostnames,
        Rules = entryMappings
          .Select(em => httpRouteRuleFactory.Create(
            app,
            em.Entry,
            em.EntryVersion
          ))
          .ToList(),
      },
    };
  }
}
