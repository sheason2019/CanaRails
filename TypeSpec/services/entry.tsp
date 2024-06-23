import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

model EntryMatcherDTO {
  key: string;
  value: string;
}

model EntryDTO {
  id: int32;
  name: string;
  description: string;
  matchers: EntryMatcherDTO[];
  appId: int32;
}

@tag("EntryController")
@route("/api/entry")
interface Entry {
  @post
  create(@body dto: EntryDTO): EntryDTO;

  @get
  list(@query appID: int32): EntryDTO[];

  @get
  @route("count")
  count(@query appID: int32): int32;

  @get
  @route("{id}")
  findById(@path id: int32): EntryDTO;

  @put
  @route("{id}")
  update(@path id: int32, @body dto: EntryDTO): int32;

  @delete
  @route("{id}")
  delete(@path id: int32): int32;

  @post
  @route("{id}/matcher")
  createMatcher(@path id: int32, @body dto: EntryMatcherDTO): void;

  @delete
  @route("{id}/matcher")
  deleteMatcher(@path id: int32, @query key: string): void;
}
