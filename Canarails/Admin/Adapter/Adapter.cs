using CanaRails.Adapter.Services;
using CanaRails.Database;
using k8s;

namespace CanaRails.Adapter;

public class ContainerAdapter
{
  private readonly Kubernetes client;
  private readonly HttpRouteService httpRouteSvr;
  private readonly KubeSvrService kubeSvr;

  public ContainerAdapter(CanaRailsContext context, AdapterConfiguration config)
  {
    client = config.Client;
    httpRouteSvr = new(client, context);
    kubeSvr = new(client, context);
  }

  // 全量同步容器
  public void Apply()
  {
    httpRouteSvr.ApplyHttpRoute();
    kubeSvr.ApplyServiceAndDeployment();
  }
}
