using CanaRails.ContainerAdapter.Services;
using k8s;

namespace CanaRails.ContainerAdapter.Test;

[TestClass]
public class UnitTest1
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

    gatewayService.Apply();
  }
}
