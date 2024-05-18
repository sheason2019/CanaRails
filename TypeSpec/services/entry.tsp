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
  appID: int32;
  deployedAt: int64;
}

model EntryMatcherDTO {
  id: int32;
  key: string;
  value: string;
  entryID: int32;
}

model ContainerDTO {
  id: int32;
  imageID: int32;
  entryID: int32;
  version: int32;
  port: int32;
  containerID: string;
  state?: string;
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
op findByID(@path id: int32): EntryDTO;

@put
@route("{id}/container")
op putContainer(@path id: int32, @body dto: ContainerDTO): ContainerDTO;

@get
@route("{id}/container")
op listContainer(@path id: int32): ContainerDTO[];

@get
@route("{id}/matcher")
op listMatcher(@path id: int32): EntryMatcherDTO[];

@put
@route("{id}/matcher")
op putMatcher(@path id: int32, @body matcher: EntryMatcherDTO): EntryMatcherDTO;

@delete
@route("{id}/matcher/{matcherID}")
op deleteMatcher(@path id: int32, @path matcherID: int32): void;

@get
@route("default")
op findDefaultEntry(@query appID: int32): Entry.EntryDTO | null;

@put
@route("default")
op putDefaultEntry(@query appID: int32, @body entryID: int32): void;
