using CanaRails.ContainerAdapter.Services;
using k8s;

namespace CanaRails.ContainerAdapter;

public class ContainerAdapter
{
  private Kubernetes client;

  public ContainerAdapter()
  {
    client = new(KubernetesClientConfiguration.InClusterConfig());
  }
}
