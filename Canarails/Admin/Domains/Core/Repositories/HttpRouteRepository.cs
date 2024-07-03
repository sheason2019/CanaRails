using System.Net;
using Admin.Domains.Core.Models;
using Admin.Domains.Core.Models.Gateway;
using k8s;
using k8s.Autorest;
using k8s.Models;

namespace Admin.Domains.Core.Repositories;

public class HttpRouteRepository(
  CanaRailsClient client
)
{
  private readonly string group = "gateway.networking.k8s.io";
  private readonly string version = "v1";
  private readonly string plural = "httproutes";

  public HttpRoute[] List()
  {
    return [];
  }

  public void Apply(
    object customObject,
    string name
  )
  {
    try
    {
      client.Kubernetes.GetNamespacedCustomObject(
        group,
        version,
        client.Configuration.Namespace,
        plural,
        name
      );
      client.Kubernetes.PatchNamespacedCustomObject(
        new V1Patch(customObject, V1Patch.PatchType.MergePatch),
        group,
        version,
        client.Configuration.Namespace,
        plural,
        name
      );
    }
    catch (Exception e) when (e is HttpOperationException exception)
    {
      if (exception.Response.StatusCode == HttpStatusCode.NotFound)
      {
        client.Kubernetes.CreateNamespacedCustomObject(
          customObject,
          group,
          version,
          client.Configuration.Namespace,
          plural
        );
        return;
      }

      throw;
    }
  }

  public void Delete(
    string name
  )
  {
    try
    {
      client.Kubernetes.GetNamespacedCustomObject(
        group,
        version,
        client.Configuration.Namespace,
        plural,
        name
      );
      client.Kubernetes.DeleteNamespacedCustomObject(
        group,
        version,
        client.Configuration.Namespace,
        plural,
        name
      );
    }
    catch (Exception e) when (e is HttpOperationException exception)
    {
      if (exception.Response.StatusCode == HttpStatusCode.NotFound)
      {
        return;
      }

      throw;
    }
  }
}