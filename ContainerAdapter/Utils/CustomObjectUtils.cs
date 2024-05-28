using System.Net;
using CanaRails.ContainerAdapter.Services;
using k8s;
using k8s.Autorest;
using k8s.Models;

namespace CanaRails.ContainerAdapter.Utils;

public class CustomObjectUtils
{
  public static void CreateOrPatch(
    Kubernetes client,
    object customObject,
    string group,
    string version,
    string plural,
    string name
  )
  {
    try
    {
      client.GetNamespacedCustomObject(
        group,
        version,
        Constant.Namespace,
        plural,
        name
      );
      client.PatchNamespacedCustomObject(
        new V1Patch(customObject, V1Patch.PatchType.MergePatch),
        group,
        version,
        Constant.Namespace,
        plural,
        name
      );
    }
    catch (Exception e) when (e is HttpOperationException exception)
    {
      if (exception.Response.StatusCode == HttpStatusCode.NotFound)
      {
        client.CreateNamespacedCustomObject(
          customObject,
          group,
          version,
          Constant.Namespace,
          plural
        );
        return;
      }

      throw;
    }
  }
}