import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

@service({
  title: "App Service",
})
@server("/api/container", "App Entry Server endpoint")
namespace Container;

model ContainerDTO {
  id: int32;
  imageID: int32;
  entryID: int32;
  version: int32;
  port: int32;
  containerID: string;
  state?: string;
}

@get
op list(@query entryId: int32): ContainerDTO[];
