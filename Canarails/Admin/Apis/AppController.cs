using Admin.Domains.Auth.Constants;
using Admin.Domains.Auth.Services;
using Admin.Domains.Core.Repositories;
using Admin.Domains.Core.Services;
using Admin.Infrastructure.Repository;
using CanaRails.Controllers;
using CanaRails.Transformer;

namespace Admin.Apis;

public class AppControllerImpl(
  AppRepository appRepository,
  AuthService authService,
  GatewayService gatewayService,
  CanaRailsContext context
) : IAppController
{
  public Task<ICollection<AppDTO>> ListAsync()
  {
    var records = context.Apps.ToList();
    var datas = records.Select(record => record.ToDTO()).ToArray();

    return Task.FromResult<ICollection<AppDTO>>(datas);
  }

  public Task<AppDTO> FindByIDAsync(int id)
  {
    var dto = appRepository.FindById(id).ToDTO();
    return Task.FromResult(dto);
  }

  public async Task<AppDTO> CreateAsync(AppDTO body)
  {
    await authService.RequireRole(Roles.Administrator);

    var record = appRepository.Create(body);
    gatewayService.Update();
    return record.ToDTO();
  }

  public async Task CreateHostnameAsync(int id, Body body)
  {
    await authService.RequireRole(Roles.Administrator);

    var app = appRepository.FindById(id);

    app.Hostnames.Add(body.Hostname);
    context.SaveChanges();

    gatewayService.Update();
  }

  public async Task DeleteHostnameAsync(int id, string hostname)
  {
    await authService.RequireRole(Roles.Administrator);

    var app = appRepository.FindById(id);

    app.Hostnames.RemoveAll(e => e == hostname);
    context.SaveChanges();

    gatewayService.Update();
  }

  public async Task PutDefaultEntryAsync(int id, int entryId)
  {
    await authService.RequireRole(Roles.Administrator);

    var app = appRepository.FindById(id);

    var queryEntry = from entries in context.Entries
                     where entries.ID.Equals(entryId)
                     select entries;
    var entry = queryEntry.First();

    app.DefaultEntry = entry;
    context.SaveChanges();

    gatewayService.Update();
  }

  public async Task<int> DeleteAsync(int id)
  {
    await authService.RequireRole(Roles.Administrator);

    appRepository.Delete(id);

    gatewayService.Update();

    return id;
  }
}
