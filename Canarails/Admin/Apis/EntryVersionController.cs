using Admin.Domains.Auth.Constants;
using Admin.Domains.Auth.Services;
using Admin.Domains.Core.Repositories;
using CanaRails.Controllers;

namespace Admin.Apis;

public class EntryVersionControllerImpl(EntryVersionRepository evRepo, AuthService authService) : IEntryVersionController
{
  public async Task<int> CreateAsync(EntryVersionDTO body)
  {
    await authService.RequireRole(Roles.Administrator);

    var record = evRepo.Create(body);
    return record.ID;
  }

  public Task<ICollection<EntryVersionDTO>> ListAsync(int entryId)
  {
    var records = evRepo.ListByEntryId(entryId);
    var dtos = records.Select(ev => new EntryVersionDTO
    {
      Id = ev.ID,
      Port = ev.Port,
      Replica = ev.Replica,
      ImageId = ev.Image.ID,
      EntryId = ev.Entry.ID,
      CreatedAt = ((DateTimeOffset)ev.CreatedAt).ToUnixTimeMilliseconds(),
    }).ToArray();

    return Task.FromResult<ICollection<EntryVersionDTO>>(dtos);
  }
}
