using k8s;

namespace CanaRails.Adapter;

public class AdapterConfiguration
{
  public required Kubernetes Client { get; set; }
}
