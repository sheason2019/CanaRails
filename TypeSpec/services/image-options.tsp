import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

@service({
  title: "Image Options Service",
})
@server("/api/image-option", "Image Option Server")
namespace ImageOption {
  model ImageOptionDTO {
    id: int32;
    appId: int32;
    imageName: string;
    createdAt: int32;
  }

  @post
  op create(dto: ImageOptionDTO): ImageOptionDTO;

  @get
  op list(@query appId: int32): ImageOptionDTO[];

  @get
  @route("{id}")
  op findByID(@path id: int32): ImageOptionDTO;
}
