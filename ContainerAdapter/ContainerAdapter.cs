using CanaRails.ContainerAdapter.Services;
using k8s;

namespace CanaRails.ContainerAdapter;

public class ContainerAdapter
{
  private Kubernetes client;
  private OrderService order;

  public ContainerAdapter()
  {
    client = new(KubernetesClientConfiguration.BuildConfigFromConfigFile());
    order = new(client);
  }

  public OrderService Order { get => order; }
}
