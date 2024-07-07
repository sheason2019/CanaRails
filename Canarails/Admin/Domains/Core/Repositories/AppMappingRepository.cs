using Admin.Domains.Core.Factories;
using Admin.Domains.Core.Models.Mappings;
using Admin.Infrastructure.Repository;

namespace Admin.Domains.Core.Repositories;

public class AppMappingRepository(
  CanaRailsContext context,
  AppMappingFactory appMappingFactory
)
{
  public AppMapping[] List()
  {
    var queryApps = from apps in context.Apps
                    join entries in context.Entries on apps.ID equals entries.App.ID
                    join entryVersions in context.EntryVersions on entries.ID equals entryVersions.Entry.ID
                    where apps.Entries.Count > 0 && entries.EntryVersions.Count > 0
                    select apps;
    var records = queryApps.ToArray();
    return records.Select(appMappingFactory.Create).ToArray();
  }
}
