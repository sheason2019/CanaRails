import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

model EntryVersionDTO {
  id: int32;
  imageId: int32;
  entryId: int32;
  port: int32;
  replica: int32;
  createdAt?: int64;
}

@tag("EntryVersionController")
@route("/api/entry-version")
interface EntryVersion {
  @post
  create(@body dto: EntryVersionDTO): int32;

  @get
  list(@query entryId: int32): EntryVersionDTO[];
}
