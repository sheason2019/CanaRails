import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

@service({
  title: "App Service",
})
@server("/api/entry-matcher", "App Entry Server endpoint")
namespace EntryMatcher;

model EntryMatcherDTO {
  id: int32;
  key: string;
  value: string;
  entryId: int32;
}

@get
op list(@query entryId: int32): EntryMatcherDTO[];

@put
op put(@body matcher: EntryMatcherDTO): int32;

@delete
@route("{id}")
op delete(@path id: int32): void;
