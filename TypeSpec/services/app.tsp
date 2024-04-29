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
    name: string;
    appID: string;
    description: string;
  }

  @post
  op create(dto: AppDTO): AppDTO;

  @get
  op list(): AppDTO[];

  @get
  @route("{id}")
  op findByID(@path id: int32): AppDTO;
}
