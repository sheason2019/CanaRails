using CanaRails.Database.Entities;

namespace CanaRails.Interfaces;

public interface IAdapter
{
  // App 资源的 CURD
  Task<App> CreateApp(App app);
  Task UpdateApp(App app);
  Task DeleteApp(App app);
  Task<App> FindAppByID(int id);
  Task<App> ListApp(int offset, int size);


  Task<Instance> CreateInstance(Instance instance);
  Task DeleteInstance(Instance instance);
  Task<Instance[]> ListInstanceByAppID(int appID);

  Task SetDefaultInstance(Instance instance);
}
