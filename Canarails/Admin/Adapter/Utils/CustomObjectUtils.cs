using System.Net;
using CanaRails.Adapter.Services;
using k8s;
using k8s.Autorest;
using k8s.Models;

namespace CanaRails.Adapter.Utils;

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

  public static void Drop(
    Kubernetes client,
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
      client.DeleteNamespacedCustomObject(
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
        return;
      }

      throw;
    }
  }
}