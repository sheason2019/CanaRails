import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

@service({
  title: "Instance Service",
})
@server("/api/instance", "Instance Server endpoint")
namespace Instance {
  model InstanceDTO {
    id: int32;
  }

  @post op create(): void;
  @get op list(): InstanceDTO[];
}
