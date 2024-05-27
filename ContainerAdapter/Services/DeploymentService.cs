using k8s;

namespace CanaRails.ContainerAdapter.Services;

public class DeploymentService(Kubernetes client)
{
  public void ApplyDeployment()
  {
    // TODO: 从数据库遍历 Entry，取出其中的 CurrentPublishOrder 应用至 Deployment
  }
}