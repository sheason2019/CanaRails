using Admin.Domains.Auth.Constants;
using Admin.Domains.Auth.Services;
using Admin.Domains.Core.Models.Entities;
using Admin.Domains.Core.Repositories;
using Admin.Domains.Core.Services;
using Admin.Infrastructure.Repository;
using Admin.Infrastructure.IDL;
using CanaRails.Transformer;

namespace Admin.Apis;

public class EntryControllerImpl(
  EntryRepository entryRepository,
  CanaRailsContext context,
  GatewayService gatewayService,
  AuthService authService
) : IEntryController
{
  public Task<int> CountAsync(int appId)
  {
    return Task.FromResult(entryRepository.CountByAppId(appId));
  }

  public async Task<EntryDTO> CreateAsync(EntryDTO body)
  {
    await authService.RequireRole(Roles.Administrator);

    var entry = entryRepository.Create(body);
    gatewayService.Update();

    return entry.ToDTO();
  }

  public async Task CreateMatcherAsync(int id, EntryMatcherDTO body)
  {
    await authService.RequireRole(Roles.Administrator);

    var entry = entryRepository.FindById(id);

    entry.EntryMatchers.Add(new EntryMatcher
    {
      Key = body.Key,
      Value = body.Value,
    });

    context.SaveChanges();

    gatewayService.Update();
  }

  public async Task<int> DeleteAsync(int entryId)
  {
    await authService.RequireRole(Roles.Administrator);

    entryRepository.Delete(entryId);

    gatewayService.Update();

    return entryId;
  }

  public async Task DeleteMatcherAsync(int id, string key)
  {
    await authService.RequireRole(Roles.Administrator);

    var entry = entryRepository.FindById(id);

    entry.EntryMatchers.RemoveAll(e => e.Key.Equals(key));
    context.SaveChanges();

    gatewayService.Update();
  }

  public Task<EntryDTO> FindByIdAsync(int id)
  {
    return Task.FromResult(entryRepository.FindById(id).ToDTO());
  }

  public Task<ICollection<EntryDTO>> ListAsync(int appId)
  {
    var records = entryRepository.ListByAppId(appId);
    return Task.FromResult<ICollection<EntryDTO>>(records.Select(e => e.ToDTO()).ToArray());
  }

  public Task<int> UpdateAsync(int id, EntryDTO body)
  {
    throw new NotImplementedException();
  }
}
