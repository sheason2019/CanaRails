import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

@service({
  title: "App Service",
})
@server("/api/publish-order", "App PublishOrder Server endpoint")
namespace PublishOrder;

model PublishOrderDTO {
  id: int32;
  imageId: int32;
  entryId: int32;
  port: int32;
  replica: int32;
  status?: string;
  createdAt?: int64;
}

@post
op create(@body dto: PublishOrderDTO): int32;

@get
op list(@query entryId: int32): PublishOrderDTO[];

@get
@route("{id}")
op getById(@path id: int32): PublishOrderDTO;
