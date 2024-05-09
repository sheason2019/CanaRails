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
@route("{appID}/matcher")
op list(@path appID: int32): AppMatcherDTO[];

@post
@route("{appID}/matcher")
op create(@path appID: int32, dto: AppMatcherDTO): AppMatcherDTO;

@get
@route("{appID}/matcher/count")
op count(@path appID: int32): int32;
