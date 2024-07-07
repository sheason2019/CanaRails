using Admin.Domains.Core.Factories;
using Admin.Domains.Core.Models.Entities;
using Admin.Domains.Core.Repositories;

namespace Admin.Domains.Core.Models.Mappings;

public class EntryMapping(
  Entry entry,
  ServiceFactory serviceFactory,
  DeploymentFactory deploymentFactory,
  KubernetesResourceRepository kubernetesResourceRepository
)
{
  public Entry Entry { get { return entry; } }

  public EntryVersion EntryVersion = entry.EntryVersions
    .OrderByDescending(ev => ev.CreatedAt)
    .First();

  // 将 App 配置应用到 Kubernetes 集群
  public void Apply()
  {
    // 应用 Deployment
    var deploy = deploymentFactory.Create(Entry, EntryVersion);
    kubernetesResourceRepository.ApplyDeployment(deploy);

    // 应用 Service
    var svr = serviceFactory.Create(Entry, EntryVersion);
    kubernetesResourceRepository.ApplyService(svr);
  }
}
