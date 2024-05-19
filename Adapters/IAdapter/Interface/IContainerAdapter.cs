

using CanaRails.Adapters.IAdapter.Models;
using CanaRails.Database.Entities;

namespace CanaRails.Adapters.IAdapter;

public interface IContainerAdapter
{
  public Task<ContainerInfo[]> GetInfos(Container[] containers);
  public Task Stop(Container[] containers);
  public Task Remove(Container[] containers);
}