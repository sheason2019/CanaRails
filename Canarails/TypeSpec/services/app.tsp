import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

import "./entry.tsp";

using TypeSpec.Http;
using TypeSpec.Rest;

model AppDTO {
  id: int32;
  name: string;
  hostnames: string[];
  defaultEntryId: int32 | null;
  description: string;
}

@tag("AppController")
@route("/api/app")
interface App {
  @post
  create(@body dto: AppDTO): AppDTO;

  @get
  list(): AppDTO[];

  @get
  @route("{id}")
  findByID(@path id: int32): AppDTO;

  @delete
  @route("{id}")
  delete(@path id: int32): int32;

  @post
  @route("{id}/hostname")
  createHostname(@path id: int32, hostname: string): void;

  @delete
  @route("{id}/hostname")
  deleteHostname(@path id: int32, @query hostname: string): void;

  @put
  @route("{id}/default-entry")
  putDefaultEntry(@path id: int32, @body entryId: int32): void;
}
