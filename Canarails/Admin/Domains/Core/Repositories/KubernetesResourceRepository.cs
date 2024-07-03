using System.Net;
using Admin.Domains.Core.Models;
using k8s;
using k8s.Autorest;
using k8s.Models;

namespace Admin.Domains.Core.Repositories;

public class KubernetesResourceRepository(
  CanaRailsClient client
)
{
  public V1Deployment[] ListDeployment()
  {
    return [];
  }

  public void ApplyDeployment(V1Deployment deploy)
  {
    try
    {
      client.Kubernetes.ReadNamespacedDeployment(
        deploy.Metadata.Name,
        client.Configuration.Namespace
      );
      client.Kubernetes.PatchNamespacedDeployment(
        new V1Patch(deploy, V1Patch.PatchType.MergePatch),
        deploy.Metadata.Name,
        client.Configuration.Namespace
      );
    }
    catch (Exception e) when (e is HttpOperationException exception)
    {
      if (exception.Response.StatusCode == HttpStatusCode.NotFound)
      {
        client.Kubernetes.CreateNamespacedDeployment(
          deploy,
          client.Configuration.Namespace
        );
      }
    }
  }

  public void DeleteDeployment(string name)
  {
    client.Kubernetes.DeleteNamespacedDeployment(
      name,
      client.Configuration.Namespace
    );
  }

  public V1Service[] ListService()
  {
    return [];
  }

  public void ApplyService(V1Service svr)
  {
    try
    {
      client.Kubernetes.ReadNamespacedService(
        svr.Metadata.Name,
        client.Configuration.Namespace
      );
      client.Kubernetes.PatchNamespacedService(
        new V1Patch(svr, V1Patch.PatchType.MergePatch),
        svr.Metadata.Name,
        client.Configuration.Namespace
      );
    }
    catch (Exception e) when (e is HttpOperationException exception)
    {
      if (exception.Response.StatusCode == HttpStatusCode.NotFound)
      {
        client.Kubernetes.CreateNamespacedService(
          svr,
          client.Configuration.Namespace
        );
        return;
      }
    }
  }

  public void DeleteService(string name)
  {
    client.Kubernetes.DeleteNamespacedService(
      name,
      client.Configuration.Namespace
    );
  }
}
