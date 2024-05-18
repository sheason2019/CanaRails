import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

@service({
  title: "App Service",
})
@server("/api/app", "App Server endpoint")
namespace AppMatcher;

model AppMatcherDTO {
  id: int32;
  host: string;
  appID: int32;
}

@get
@route("{appId}/matcher")
op list(@path appId: int32): AppMatcherDTO[];

@post
@route("{appId}/matcher")
op create(@path appId: int32, @body dto: AppMatcherDTO): AppMatcherDTO;

@get
@route("{appId}/matcher/count")
op count(@path appId: int32): int32;

@delete
@route("{appId}/matcher/{matcherId}")
op delete(@path appId: int32, matcherId: int32): int32;
