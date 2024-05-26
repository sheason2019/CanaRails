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

  [JsonPropertyName("namespace")]
  public string? Namespace { get; set; }
}

public class HttpRouteRule
{
  [JsonPropertyName("matches")]
  public List<HttpRouteRuleMatch>? Matches { get; set; }

  [JsonPropertyName("filters")]
  public List<HttpRouteRuleFilter>? Filters { get; set; }

  [JsonPropertyName("backendRefs")]
  public List<HttpRouteRuleBackendRef>? BackendRefs { get; set; }
}

public class HttpRouteRuleMatch
{
  [JsonPropertyName("path")]
  public HTTPPathMatch? Path { get; set; }

  [JsonPropertyName("headers")]
  public List<HTTPHeaderMatch>? Headers { get; set; }
}

public class HTTPPathMatch
{
  [JsonPropertyName("type")]
  public string? Type { get; set; }
  [JsonPropertyName("value")]
  public string? Value { get; set; }
}

public class HTTPHeaderMatch
{
  [JsonPropertyName("type")]
  public string? Type { get; set; }

  [JsonPropertyName("name")]
  public string? Name { get; set; }

  [JsonPropertyName("value")]
  public string? Value { get; set; }
}

public class HttpRouteRuleFilter
{
  [JsonPropertyName("type")]
  public string? Type { get; set; }

  [JsonPropertyName("requestHeaderModifier")]
  public object? RequestHeaderModifier { get; set; }
}

public class HttpRouteRuleRequestHeaderModifier
{
  [JsonPropertyName("add")]
  public List<HttpRouteRuleRequestHeaderModifierAdd>? Add { get; set; }
}

public class HttpRouteRuleRequestHeaderModifierAdd
{
  [JsonPropertyName("name")]
  public string? Name { get; set; }

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
