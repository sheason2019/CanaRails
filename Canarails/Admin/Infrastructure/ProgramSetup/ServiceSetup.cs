using Admin.Domains.Core.Models;
using Admin.Domains.Core.Models.Configurations;
using Admin.Domains.Core.Services;
using Admin.Infrastructure.Constants;
using k8s;

namespace Admin.Infrastructure.ProgramSetup;

// 初始化服务和依赖关系
public static class ServiceSetup
{
  // 初始化 Kuberneters 模块
  public static void AddCanarailsKuberneters(this IServiceCollection services)
  {
    services.AddScoped(config =>
    {
      var clientConfig = EnvVariables.CANARAILS_CLIENT_CONFIG;
      if (clientConfig == "IN_CLUSTER")
      {
        return new CanaRailsClient
        {
          Kubernetes = new Kubernetes(
            KubernetesClientConfiguration.InClusterConfig()
          ),
          Configuration = new CanaRailsConfiguration(),
        };
      }
      return new CanaRailsClient
      {
        Kubernetes = new Kubernetes(
          KubernetesClientConfiguration.BuildConfigFromConfigFile(
            "/etc/rancher/k3s/k3s.yaml"
          )
        ),
        Configuration = new CanaRailsConfiguration(),
      };
    });
    services.AddScoped<GatewayService>();
  }
}
