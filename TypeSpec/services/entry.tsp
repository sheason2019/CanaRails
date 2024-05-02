import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

import "./image.tsp";

using TypeSpec.Http;
using TypeSpec.Rest;

@service({
  title: "App Service",
})
@server("/api/entry", "App Entry Server endpoint")
namespace Entry {
  model EntryDTO {
    id: int32;
    name: string;
    port: int32;
    description: string;
    appID: int32;
    imageID: int32;
  }

  @post
  op create(dto: EntryDTO): EntryDTO;

  @get
  op list(@query appID: int32): EntryDTO[];

  @get
  @route("{id}")
  op findByID(@path id: int32): EntryDTO;
}
