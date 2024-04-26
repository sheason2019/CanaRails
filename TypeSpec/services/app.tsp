import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

@service({
  title: "App Service",
})
@server("/api/app", "App Server endpoint")
namespace App {
  model AppDTO {
    id: int32;
  }

  @post op create(): void;
  @get op list(): AppDTO[];
}
