import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

@service({
  title: "Service Service",
})
@server("/api/service", "Service Server endpoint")
namespace Service {
  model ServiceDTO {
    id: int32;
  }

  @post op create(): void;
  @get op list(): ServiceDTO[];
}
