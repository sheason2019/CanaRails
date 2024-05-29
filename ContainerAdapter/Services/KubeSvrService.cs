using System.Net;
using CanaRails.ContainerAdapter.Utils;
using CanaRails.Database;
using CanaRails.Database.Entities;
using k8s;
using k8s.Autorest;
using k8s.Models;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.ContainerAdapter.Services;

public class KubeSvrService(
  Kubernetes client,
  CanaRailsContext context
)
{
  public void ApplyServiceAndDeployment()
  {
    var queryEntries = from entries in context.Entries
                       where entries.CurrentPublishOrder != null
                       select entries;
    var validEntries = queryEntries
      .Include(e => e.CurrentPublishOrder)
      .ToArray();

    var svrList = client.ListNamespacedService(
      Constant.Namespace
    );
    var deleteSvrSet = new HashSet<string>();
    foreach (var svr in svrList.Items)
    {
      var id = ParseIdUtils.ParseEntryIdByServiceName(svr.Metadata.Name);
      if (id != null)
      {
        deleteSvrSet.Add(svr.Metadata.Name);
      }
    }

    var deployList = client.ListNamespacedDeployment(
      Constant.Namespace
    );
    var deleteDeploySet = new HashSet<string>();
    foreach (var deploy in deployList.Items)
    {
      var id = ParseIdUtils.ParseEntryIdByDeployName(deploy.Metadata.Name);
      if (id != null)
      {
        deleteDeploySet.Add(deploy.Metadata.Name);
      }
    }

    foreach (var entry in validEntries)
    {
      ApplyDeploymentByEntry(entry);
      ApplyServiceByEntry(entry);
      deleteSvrSet.Remove($"canarails-service-by-entry-{entry.ID}");
      deleteDeploySet.Remove($"canarails-deployment-by-entry-{entry.ID}");
    }

    foreach (var name in deleteSvrSet)
    {
      client.DeleteNamespacedService(
        name,
        Constant.Namespace
      );
    }
    foreach (var name in deleteDeploySet)
    {
      client.DeleteNamespacedDeployment(
        name,
        Constant.Namespace
      );
    }
  }

  public void ApplyServiceByEntry(Entry entry)
  {
    // 将 Entry 映射为 Kubenetes Service
    var svr = new V1Service
    {
      Metadata = {
        Name = $"canarails-service-by-entry-{entry.ID}",
      },
      Spec = {
        Selector = {
          {"canarails-deployment-by-entry",$"{entry.ID}"}
        },
        Ports = [
          new V1ServicePort {
            Name = "main-expose",
            Protocol = "TCP",
            Port = entry.CurrentPublishOrder!.Port,
            TargetPort = entry.CurrentPublishOrder!.Port,
          }
        ],
      },
    };

    try
    {
      client.ReadNamespacedService(
        svr.Metadata.Name,
        Constant.Namespace
      );
      client.PatchNamespacedService(
        new V1Patch(svr, V1Patch.PatchType.MergePatch),
        svr.Metadata.Name,
        Constant.Namespace
      );
    }
    catch (Exception e) when (e is HttpOperationException exception)
    {
      if (exception.Response.StatusCode == HttpStatusCode.NotFound)
      {
        client.CreateNamespacedService(
          svr,
          Constant.Namespace
        );
        return;
      }

      throw;
    }
  }
  
  public void ApplyDeploymentByEntry(Entry entry)
  {
    var deploy = new V1Deployment
    {
      Metadata = {
        Name = $"canarails-deployment-by-entry-{entry.ID}",
        Labels = {
          {"canarails-deployment-by-entry", $"{entry.ID}"},
        },
      },
      Spec = {
        Replicas = entry.CurrentPublishOrder!.Replica,
        Selector = {
          MatchLabels = {
            {"canarails-deployment-by-entry", $"{entry.ID}"},
          },
        },
        Template = {
          Metadata = {
            Labels = {
              {"canarails-deployment-by-entry", $"{entry.ID}"},
            },
          },
          Spec = {
            Containers = [
              new V1Container {
                Name = entry.CurrentPublishOrder!.Image.ImageName,
                Image = entry.CurrentPublishOrder!.Image.ImageName,
                Ports = [
                  new V1ContainerPort
                  {
                    ContainerPort = 80,
                  },
                ],
              },
            ],
          },
        },
      }
    };

    try
    {
      client.ReadNamespacedDeployment(
        deploy.Metadata.Name,
        Constant.Namespace
      );
      client.PatchNamespacedDeployment(
        new V1Patch(deploy, V1Patch.PatchType.MergePatch),
        deploy.Metadata.Name,
        Constant.Namespace
      );
    }
    catch (Exception e) when (e is HttpOperationException exception)
    {
      if (exception.Response.StatusCode == HttpStatusCode.NotFound)
      {
        client.CreateNamespacedDeployment(
          deploy,
          Constant.Namespace
        );
      }
    }
  }
}