using CanaRails.Adapters.IAdapter;
using CanaRails.Adapters.IAdapter.Models;
using CanaRails.Database.Entities;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace CanaRails.Adapters.DockerAdapter;

public class ContainerAdapter(DockerClient client) : IContainerAdapter
{
  public Task<ContainerInfo[]> GetInfos(Container[] containers)
  {
    throw new NotImplementedException();
  }

  public async Task Remove(Container[] containers)
  {
    foreach (var container in containers)
    {
      await client.Containers.RemoveContainerAsync(
        container.ContainerID,
        new ContainerRemoveParameters { },
        CancellationToken.None
      );
    }
  }

  public async Task Stop(Container[] containers)
  {
    foreach (var container in containers)
    {
      await client.Containers.StopContainerAsync(
        container.ContainerID,
        new ContainerStopParameters { },
        CancellationToken.None
      );
    }
  }
}
