using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace CanaRails.Adapter.Models;

public class Gateway : KubernetesObject, IMetadata<V1ObjectMeta>
{
  [JsonPropertyName("metadata")]
  public required V1ObjectMeta Metadata { get; set; }

  [JsonPropertyName("spec")]
  public GatewaySpec? Spec { get; set; }
}

public class GatewaySpec
{
  [JsonPropertyName("gatewayClassName")]
  public string? GatewayClassName { get; set; }

  [JsonPropertyName("listeners")]
  public List<GatewayListener>? Listeners { get; set; }
}

public class GatewayListener
{
  [JsonPropertyName("protocol")]
  public string? Protocol { get; set; }

  [JsonPropertyName("hostname")]
  public string? Hostname { get; set; }

  [JsonPropertyName("port")]
  public int Port { get; set; }

  [JsonPropertyName("name")]
  public string? Name { get; set; }

  [JsonPropertyName("allowedRoutes")]
  public GatewayAllowedRoutes? AllowedRoutes { get; set; }
}

public class GatewayAllowedRoutes
{
  [JsonPropertyName("namespaces")]
  public GatewayAllowedRoutesNamespaces? Namespaces { get; set; }
}

public class GatewayAllowedRoutesNamespaces
{
  [JsonPropertyName("from")]
  public string? From { get; set; }
}
