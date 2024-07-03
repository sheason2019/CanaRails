using Admin.Domains.Core.Factories;
using Admin.Domains.Core.Repositories;
using Admin.Domains.Core.Services;

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
  }
}
