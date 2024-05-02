
using CanaRails.Services;
using CanaRails.Transformer;

namespace CanaRails.Controllers.Entry;

public class EntryControllerImpl(EntryService service) : IEntryController
{
  public async Task<EntryDTO> CreateAsync(Body body)
  {
    var entry = await service.CreateEntry(body.Dto);
    return entry.ToDTO();
  }

  public Task<EntryDTO> FindByIDAsync(int id)
  {
    throw new NotImplementedException();
  }

  public Task<ICollection<EntryDTO>> ListAsync(int appID)
  {
    throw new NotImplementedException();
  }
}