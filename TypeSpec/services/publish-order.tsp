import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

model PublishOrderDTO {
  id: int32;
  imageId: int32;
  entryId: int32;
  port: int32;
  replica: int32;
  status?: string;
  createdAt?: int64;
}

@tag("PublishOrderController")
@route("/api/publish-order")
interface PublishOrder {
  @post
  create(@body dto: PublishOrderDTO): int32;

  @get
  list(@query entryId: int32): PublishOrderDTO[];

  @get
  @route("{id}")
  getById(@path id: int32): PublishOrderDTO;
}
