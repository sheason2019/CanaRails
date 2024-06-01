using System.Net;
using CanaRails.Adapter.Utils;
using CanaRails.Database;
using CanaRails.Database.Entities;
using k8s;
using k8s.Autorest;
using k8s.Models;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Adapter.Services;

public class KubeSvrService(
  Kubernetes client,
  CanaRailsContext context
)
{
  public void ApplyServiceAndDeployment()
  {
    var queryEntries = from entries in context.Entries
                       join publishOrders in context.PublishOrders on entries.ID equals publishOrders.Entry.ID
                       where publishOrders.Status == PublishOrderStatus.Approval
                       select entries;
    var validEntries = queryEntries
      .Include(e => e.PublishOrders)
      .ThenInclude(e => e.Image)
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
    var curOrder = entry
      .PublishOrders
      .Where(e => e.Status == PublishOrderStatus.Approval)
      .First();

    // 将 Entry 映射为 Kubenetes Service
    var svr = new V1Service
    {
      Metadata = new V1ObjectMeta
      {
        Name = $"canarails-service-by-entry-{entry.ID}",
      },
      Spec = new V1ServiceSpec
      {
        Selector = new Dictionary<string, string>
        {
          {"canarails-deployment-by-entry",$"{entry.ID}"}
        },
        Ports = [
          new V1ServicePort {
            Name = "main-expose",
            Protocol = "TCP",
            Port = curOrder.Port,
            TargetPort = curOrder.Port,
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
    var curOrder = entry
      .PublishOrders
      .Where(e => e.Status == PublishOrderStatus.Approval)
      .First();

    V1Deployment deploy = new()
    {
      Metadata = new V1ObjectMeta
      {
        Name = $"canarails-deployment-by-entry-{entry.ID}",
        Labels = new Dictionary<string, string>
        {
          {"canarails-deployment-by-entry", entry.ID.ToString()},
        },
      },
      Spec = new V1DeploymentSpec
      {
        Replicas = curOrder.Replica,
        Selector = new V1LabelSelector
        {
          MatchLabels = new Dictionary<string, string>
          {
            {"canarails-deployment-by-entry", entry.ID.ToString()},
          },
        },
        Template = new V1PodTemplateSpec
        {
          Metadata = new V1ObjectMeta
          {
            Labels = new Dictionary<string, string>
            {
              {"canarails-deployment-by-entry", entry.ID.ToString()},
            },
          },
          Spec = new V1PodSpec
          {
            Containers = [
              new V1Container {
                Name = $"image-{curOrder.Image.ID}",
                Image = curOrder.Image.ImageName,
                Ports = [
                  new V1ContainerPort
                  {
                    ContainerPort = curOrder.Port,
                  },
                ],
              },
            ],
          },
        },
      },
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