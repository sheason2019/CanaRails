import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

import "./entry.tsp";

using TypeSpec.Http;
using TypeSpec.Rest;

@service({
  title: "App Service",
})
@server("/api/app", "App Server endpoint")
namespace App;

model AppDTO {
  id: int32;
  name: string;
  hostnames: string[];
  defaultEntryId: int32 | null;
  description: string;
}

@post
op create(@body dto: AppDTO): AppDTO;

@get
op list(): AppDTO[];

@get
@route("{id}")
op findByID(@path id: int32): AppDTO;

@post
@route("{id}/hostname")
op createHostname(@path id: int32, hostname: string): void;

@delete
@route("{id}/hostname")
op deleteHostname(@path id: int32, @query hostname: string): void;

@put
@route("{id}/default-entry")
op putDefaultEntry(@path id: int32, @body entryId: int32): void;
