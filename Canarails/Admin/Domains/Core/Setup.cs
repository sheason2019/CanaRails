using Admin.Domains.Core.Factories;
using Admin.Domains.Core.Models;
using Admin.Domains.Core.Models.Configurations;
using Admin.Domains.Core.Repositories;
using Admin.Domains.Core.Services;
using Admin.Infrastructure.Constants;
using k8s;

namespace Admin.Domains.Core;

public static class Setup
{
  public static void AddCoreDomain(this IServiceCollection services)
  {
    // Repository
    services.AddScoped<AppMappingRepository>();
    services.AddScoped<AppRepository>();
    services.AddScoped<EntryRepository>();
    services.AddScoped<HttpRouteRepository>();
    services.AddScoped<ImageRepository>();
    services.AddScoped<KubernetesResourceRepository>();
    services.AddScoped<GatewayService>();
    services.AddScoped<EntryVersionRepository>();

    // Factory
    services.AddScoped<AppMappingFactory>();
    services.AddScoped<DeploymentFactory>();
    services.AddScoped<EntryMappingFactory>();
    services.AddScoped<HttpRouteFactory>();
    services.AddScoped<HttpRouteRuleFactory>();
    services.AddScoped<ResourceNameFactory>();
    services.AddScoped<ServiceFactory>();
    services.AddScoped<DtoFactory>();

    // Kubernetes
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
