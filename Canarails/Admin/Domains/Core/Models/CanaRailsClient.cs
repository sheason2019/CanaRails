using Admin.Domains.Core.Models.Configurations;
using k8s;

namespace Admin.Domains.Core.Models;

public class CanaRailsClient
{
  public required Kubernetes Kubernetes { get; set; }

  public required CanaRailsConfiguration Configuration { get; set; }
}
