using Admin.Domains.Core.Factories;
using Admin.Domains.Core.Repositories;
using k8s.Models;

namespace Admin.Domains.Core.Services;

public class GatewayService(
  AppMappingRepository appMappingRepository,
  KubernetesResourceRepository kubernetesResourceRepository,
  HttpRouteRepository httpRouteRepository,
  ResourceNameFactory resourceNameFactory
)
{
  // 全量更新网关配置
  public void Update()
  {
    var appMappings = appMappingRepository.List();
    var appIdSet = appMappings.Select(am => am.App.ID).ToHashSet();

    // 遍历 Deployment 得到移除内容
    var deployments = kubernetesResourceRepository.ListDeployment();
    foreach (var deploy in deployments)
    {
      if (resourceNameFactory.GetId(deploy.Name()) != null)
      {
        var appId = int.Parse(deploy.Labels()["app-id"]);
        if (!appIdSet.Contains(appId))
        {
          kubernetesResourceRepository.DeleteDeployment(deploy.Name());
        }
      }
    }

    // 遍历 Service 得到移除内容
    var services = kubernetesResourceRepository.ListService();
    foreach (var service in services)
    {
      if (resourceNameFactory.GetId(service.Name()) != null)
      {
        var appId = int.Parse(service.Labels()["app-id"]);
        if (!appIdSet.Contains(appId))
        {
          kubernetesResourceRepository.DeleteService(service.Name());
        }
      }
    }

    // 遍历 HttpRoute 得到移除内容
    var httpRoutes = httpRouteRepository.List();
    foreach (var httpRoute in httpRoutes)
    {
      if (resourceNameFactory.GetId(httpRoute.Name()) != null)
      {
        var appId = int.Parse(httpRoute.Labels()["app-id"]);
        if (!appIdSet.Contains(appId))
        {
          httpRouteRepository.Delete(httpRoute.Name());
        }
      }
    }

    foreach (var appMapping in appMappings)
    {
      appMapping.Apply();
    }
  }
}
