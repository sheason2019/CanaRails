using Admin.Domains.Core.Models.Entities;
using Admin.Domains.Core.Models.Gateway;

namespace Admin.Domains.Core.Factories;

public class HttpRouteRuleFactory(
  ResourceNameFactory resourceNameFactory
)
{
  public HttpRouteRule Create(App app, Entry entry, EntryVersion ev)
  {
    var headerModifier = new HTTPHeaderFilter
    {
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
    };

    return new HttpRouteRule
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
          RequestHeaderModifier = headerModifier,
        },
        new HttpRouteRuleFilter {
          Type = "ResponseHeaderModifier",
          ResponseHeaderModifier = headerModifier,
        }
         ],
      BackendRefs = [
        new HttpRouteRuleBackendRef {
          Name = resourceNameFactory.GetName(entry.ID),
          Port = ev.Port,
        },
      ],
    };
  }
}
