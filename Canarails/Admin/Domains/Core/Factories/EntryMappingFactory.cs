using Admin.Domains.Core.Models.Entities;
using Admin.Domains.Core.Models.Mappings;
using Admin.Domains.Core.Repositories;

namespace Admin.Domains.Core.Factories;

public class EntryMappingFactory(
  KubernetesResourceRepository kubernetesResourceRepository,
  DeploymentFactory deploymentFactory,
  ServiceFactory serviceFactory
)
{
  public EntryMapping Create(Entry entry)
  {
    return new EntryMapping(
      entry,
      serviceFactory,
      deploymentFactory,
      kubernetesResourceRepository
    );
  }
}
