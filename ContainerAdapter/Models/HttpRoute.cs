using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace CanaRails.ContainerAdapter.Models;

public class HttpRoute : KubernetesObject, IMetadata<V1ObjectMeta>
{
  [JsonPropertyName("metadata")]
  public required V1ObjectMeta Metadata { get; set; }

  [JsonPropertyName("spec")]
  public HttpRouteSpec? Spec { get; set; }
}

public class HttpRouteSpec
{
  [JsonPropertyName("parentRefs")]
  public List<HttpRouteParentRef> ParentRefs { get; set; } = [];

  [JsonPropertyName("hostnames")]
  public List<string> Hostnames { get; set; } = [];

  [JsonPropertyName("rules")]
  public List<HttpRouteRule> Rules { get; set; } = [];
}

public class HttpRouteParentRef
{
  [JsonPropertyName("name")]
  public string? Name { get; set; }
}

public class HttpRouteRule
{
  [JsonPropertyName("matches")]
  public List<HttpRouteRuleMatch>? Matches { get; set; }

  [JsonPropertyName("backendRefs")]
  public List<HttpRouteRuleBackendRef>? BackendRefs { get; set; }
}

public class HttpRouteRuleMatch
{
  [JsonPropertyName("path")]
  public HttpRouteRuleMatchPath? Path { get; set; }
}

public class HttpRouteRuleMatchPath
{
  [JsonPropertyName("type")]
  public string? Type { get; set; }
  [JsonPropertyName("value")]
  public string? Value { get; set; }
}

public class HttpRouteRuleBackendRef
{
  [JsonPropertyName("name")]
  public string? Name { get; set; }

  [JsonPropertyName("port")]
  public int? Port { get; set; }

  [JsonPropertyName("weight")]
  public int? Weight { get; set; }
}
