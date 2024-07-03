using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Admin.Domains.Core.Models.Gateway;

public class CustomResourceList<T> : KubernetesObject
{
  [JsonPropertyName("metadata")]
  public required V1ListMeta Metadata { get; set; }
  [JsonPropertyName("items")]
  public List<T>? Items { get; set; }
}
