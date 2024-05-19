using CanaRails.Adapters.IAdapter;
using CanaRails.Adapters.IAdapter.Models;
using CanaRails.Database.Entities;
using Docker.DotNet;

namespace CanaRails.Adapters.DockerAdapter;

public class ContainerAdapter(DockerClient client) : IContainerAdapter
{
  public Task<ContainerInfo[]> GetInfos(Container[] containers)
  {
    throw new NotImplementedException();
  }

  public Task Remove(Container[] containers)
  {
    throw new NotImplementedException();
  }

  public Task Stop(Container[] containers)
  {
    throw new NotImplementedException();
  }
}
