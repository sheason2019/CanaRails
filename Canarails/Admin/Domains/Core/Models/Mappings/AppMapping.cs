using Admin.Domains.Core.Models.Entities;
using k8s.Models;
using Admin.Domains.Core.Factories;
using Admin.Domains.Core.Repositories;

namespace Admin.Domains.Core.Models.Mappings;

// App 映射
public class AppMapping(
  App app,
  EntryMapping[] entryMappings,
  HttpRouteFactory httpRouteFactory,
  HttpRouteRepository httpRouteRepository
)
{
  public App App { get { return app; } }

  public void Apply()
  {
    foreach (var entryMapping in entryMappings)
    {
      entryMapping.Apply();
    }

    var httpRoute = httpRouteFactory.Create(app, entryMappings);
    httpRouteRepository.Apply(
      httpRoute,
      httpRoute.Name()
    );
  }
}
