using CanaRails.Database.Entities;
using CanaRails.Interfaces;

namespace CanaRails.Adapters.DockerAdapter;

public class DockerAdapter : IAdapter
{
  public Task<App> CreateApp(App app)
  {
    throw new NotImplementedException();
  }

  public Task<Instance> CreateInstance(Instance instance)
  {
    throw new NotImplementedException();
  }

  public Task DeleteApp(App app)
  {
    throw new NotImplementedException();
  }

  public Task DeleteInstance(Instance instance)
  {
    throw new NotImplementedException();
  }

  public Task<App> FindAppByID(int id)
  {
    throw new NotImplementedException();
  }

  public Task<Instance[]> ListInstanceByAppID(int appID)
  {
    throw new NotImplementedException();
  }

  public Task SetDefaultInstance(Instance instance)
  {
    throw new NotImplementedException();
  }

  public Task UpdateApp(App app)
  {
    throw new NotImplementedException();
  }

  Task<App> IAdapter.ListApp(int offset, int size)
  {
    throw new NotImplementedException();
  }
}
