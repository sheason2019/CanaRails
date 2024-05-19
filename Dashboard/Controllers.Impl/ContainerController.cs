
using CanaRails.Controllers.Container;

namespace CanaRails.Controllers.Impl;

public class ContainerControllerImpl() : IContainerController
{
  public Task<ICollection<ContainerDTO>> ListAsync(int entryId)
  {
    throw new NotImplementedException();
  }
}
