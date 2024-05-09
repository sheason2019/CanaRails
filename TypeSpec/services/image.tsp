import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

@service({
  title: "App Image Service",
})
@server("/api/image", "App Image Server endpoint")
namespace Image {
  model ImageDTO {
    id: int32;
    registry: string;
    imageName: string;
    tagName: string;
    appID: int32;
  }

  @post
  op create(dto: ImageDTO): ImageDTO;

  @get
  op list(@query appID: int32): ImageDTO[];

  @get
  @route("count")
  op count(@query appID: int32): int32;

  @get
  @route("{id}")
  op findByID(@path id: int32): ImageDTO;
}
