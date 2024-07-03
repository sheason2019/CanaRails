using Admin.Domains.Core.Models.Entities;
using Admin.Infrastructure.Repository;
using k8s.Models;

namespace Admin.Domains.Core.Factories;

public class DeploymentFactory(
  CanaRailsContext context,
  ResourceNameFactory resourceNameFactory
)
{
  public V1Deployment Create(Entry entry, EntryVersion ev)
  {
    context.Entry(entry).Reference(e => e.App).Load();
    context.Entry(ev).Reference(e => e.Image).Load();

    var labels = new Dictionary<string, string>
    {
      {"app-id", entry.App.ID.ToString()},
      {"entry-id", entry.ID.ToString()},
    };

    return new V1Deployment
    {
      Metadata = new V1ObjectMeta
      {
        Name = resourceNameFactory.GetName(entry.ID),
        Labels = labels,
      },
      Spec = new V1DeploymentSpec
      {
        Replicas = ev.Replica,
        Selector = new V1LabelSelector { MatchLabels = labels },
        Template = new V1PodTemplateSpec
        {
          Metadata = new V1ObjectMeta { Labels = labels },
          Spec = new V1PodSpec
          {
            Containers = [
              new V1Container {
                Name = $"image-{ev.Image.ID}",
                Image = ev.Image.ImageName,
                Ports = [
                  new V1ContainerPort
                  {
                    ContainerPort = ev.Port,
                  },
                ],
              },
            ],
          },
        },
      },
    };
  }
}
