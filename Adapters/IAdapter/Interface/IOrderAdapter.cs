using CanaRails.Adapters.IAdapter.Models;
using CanaRails.Database.Entities;

namespace CanaRails.Adapters.IAdapter;

public interface IOrderAdapter
{
  public Task<string[]> Start(PublishOrder order);
  public Task<string[]> Stop(PublishOrder order);
}
