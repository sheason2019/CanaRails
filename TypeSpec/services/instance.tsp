import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

@service({
  title: "Instance Service",
})
@server("/api/instance", "Instance Server endpoint")
namespace Instance {
  model InstanceDTO {
    id: int32;
    name: string;
    registry: string;
    imageName: string;
    port: string;
    env: EnvDTO[];

    appID: int32;
  }

  model EnvDTO {
    key: string;
    value: string;
  }

  @post op create(dto: InstanceDTO): InstanceDTO;
  @get op list(): InstanceDTO[];
}
