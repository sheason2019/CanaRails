using CanaRails.Adapters.IAdapter;
using CanaRails.Adapters.IAdapter.Models;
using CanaRails.Database.Entities;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace CanaRails.Adapters.DockerAdapter;

public class OrderAdapter(DockerClient client) : IOrderAdapter
{
  public async Task<string[]> Start(PublishOrder order)
  {
    // 创建 service container 容器
    var containerInfos = new ContainerInfo[order.Replica + 1];
    for (var i = 0; i < order.Replica; i++)
    {
      var container = await client.Containers.CreateContainerAsync(
        new CreateContainerParameters
        {
          Image = order.Image.ImageName,
          HostConfig = new HostConfig
          {
            RestartPolicy = new RestartPolicy
            {
              Name = RestartPolicyKind.Always,
            }
          }
        }
      );
      var inspect = await client.Containers.InspectContainerAsync(
        container.ID
      );

      containerInfos[i] = new ContainerInfo
      {
        ContainerId = container.ID,
        IPAddress = inspect.NetworkSettings.IPAddress,
      };
    }

    // 创建 ingress container 容器实现负载均衡


    throw new NotImplementedException();
  }

  public Task<string[]> Stop(PublishOrder order)
  {
    throw new NotImplementedException();
  }
}
