using CanaRails.Adapter.Services;
using CanaRails.Database;
using k8s;

namespace CanaRails.Adapter.Test;

[TestClass]
public class ApplyTest
{
  [TestMethod]
  public void TestUpdateGateway()
  {
    Kubernetes client = new(
      KubernetesClientConfiguration.BuildConfigFromConfigFile(
        Environment.GetEnvironmentVariable("KUBECONFIG")
      )
    );
    var gatewayService = new GatewayService(client);

    gatewayService.ApplyGateway();
  }

  [TestMethod]
  public void TestApply()
  {
    var adapter = new ContainerAdapter(
      new CanaRailsContext(),
      new AdapterConfiguration
      {
        Client = new Kubernetes(
          KubernetesClientConfiguration.BuildConfigFromConfigFile(
            "/etc/rancher/k3s/k3s.yaml"
          )
        )
      }
    );
    adapter.Apply();
  }
}
