import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

@service({
  title: "App Service",
})
@server("/api/entry", "App Entry Server endpoint")
namespace Entry;

model EntryDTO {
  id: int32;
  name: string;
  description: string;
  appId: int32;
}

@post
op create(@body dto: EntryDTO): EntryDTO;

@get
op list(@query appID: int32): EntryDTO[];

@get
@route("count")
op count(@query appID: int32): int32;

@get
@route("{id}")
op findById(@path id: int32): EntryDTO;

@put
@route("{id}")
op update(@path id: int32, @body dto: EntryDTO): int32;
