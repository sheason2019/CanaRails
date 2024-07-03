using Admin.Domains.Core.Models.Entities;
using Admin.Domains.Core.Models.Mappings;
using Admin.Domains.Core.Repositories;
using Admin.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Admin.Domains.Core.Factories;

public class AppMappingFactory(
  CanaRailsContext context,
  HttpRouteFactory httpRouteFactory,
  EntryMappingFactory entryMappingFactory,
  HttpRouteRepository customObjectRepository
)
{
  public AppMapping Create(App app)
  {
    var entries = context.Entry(app)
      .Collection(e => e.Entries)
      .Query()
      .Where(e => e.EntryVersions.Count > 0)
      .Include(e => e.EntryVersions)
      .ToArray();


    var entryMappings = entries
      .Select(entryMappingFactory.Create)
      .ToArray();

    return new AppMapping(
      app,
      entryMappings,
      httpRouteFactory,
      customObjectRepository
    );
  }
}
