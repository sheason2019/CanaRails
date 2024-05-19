
using CanaRails.Controllers.EntryMatcher;

namespace CanaRails.Controllers.Impl;

public class EntryMatcherControllerImpl() : IEntryMatcherController
{
  public Task DeleteAsync(int id)
  {
    throw new NotImplementedException();
  }

  public Task<ICollection<EntryMatcherDTO>> ListAsync(int entryId)
  {
    throw new NotImplementedException();
  }

  public Task<int> PutAsync(EntryMatcherDTO body)
  {
    throw new NotImplementedException();
  }
}
