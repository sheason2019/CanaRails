using CanaRails.ContainerAdapter.Services;
using CanaRails.Database;
using k8s;

namespace CanaRails.ContainerAdapter;

public class ContainerAdapter
{
  private readonly Kubernetes client;
  private readonly GatewayService gatewaySvr;
  private readonly HttpRouteService httpRouteSvr;
  private readonly KubeSvrService kubeSvr;

  public ContainerAdapter(CanaRailsContext context)
  {
    client = new(KubernetesClientConfiguration.InClusterConfig());
    gatewaySvr = new(client);
    httpRouteSvr = new(client, context);
    kubeSvr = new(client, context);
  }

  // 全量同步容器
  public void Apply()
  {
    gatewaySvr.ApplyGateway();
    httpRouteSvr.ApplyHttpRoute();
    kubeSvr.ApplyServiceAndDeployment();
  }
}
