using Admin.Domains.Core.Models.Entities;
using k8s.Models;

namespace Admin.Domains.Core.Factories;

public class ServiceFactory(
  ResourceNameFactory resourceNameFactory
)
{
  public V1Service Create(Entry entry, EntryVersion ev)
  {
     var labels = new Dictionary<string, string>
    {
      {"app-id", entry.App.ID.ToString()},
      {"entry-id", entry.ID.ToString()},
    };

    return new V1Service
    {
      Metadata = new V1ObjectMeta
      {
        Name = resourceNameFactory.GetName(entry.ID),
      },
      Spec = new V1ServiceSpec
      {
        Selector = labels,
        Ports = [
          new V1ServicePort {
            Name = "main-expose",
            Protocol = "TCP",
            Port = ev.Port,
            TargetPort = ev.Port,
          }
        ],
      },
    };
  }
}
