import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

model ImageDTO {
  id: int32;
  registry: string;
  imageName: string;
  appId: int32;
  ready: boolean;
  createdAt: int64;
}

@tag("ImageController")
@route("/api/image")
interface Image {
  @post
  create(@body dto: ImageDTO): ImageDTO;

  @get
  list(@query appID: int32): ImageDTO[];

  @get
  @route("count")
  count(@query appID: int32): int32;

  @get
  @route("{id}")
  findById(@path id: int32): ImageDTO;
}
