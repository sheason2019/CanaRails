
using CanaRails.Adapters.IAdapter;
using CanaRails.Database;
using CanaRails.Exceptions;
using CanaRails.Services;
using CanaRails.Transformer;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Controllers.Entry;

public class EntryControllerImpl(
  AppService appService,
  EntryService entryService,
  ImageService imageService,
  ContainerService containerService,
  IAdapter adapter,
  CanaRailsContext context
) : IEntryController
{
  public Task<int> CountAsync(int appID)
  {
    return entryService.CountAsync(appID);
  }

  public async Task<EntryDTO> CreateAsync(EntryDTO body)
  {
    var entry = await entryService.CreateEntry(body);
    return await entry.ToDTO().AddDeployInfo(entry, adapter);
  }

  public async Task<EntryDTO> FindByIDAsync(int id)
  {
    var entry = await entryService.FindByIDAsync(id);
    return await entry.ToDTO().AddDeployInfo(entry, adapter);
  }

  public async Task<ICollection<EntryDTO>> ListAsync(int appID)
  {
    var records = await entryService.ListAsync(appID);
    return await Task.WhenAll(records.Select(
      e => e.ToDTO().AddDeployInfo(e, adapter)
    ));
  }

  public async Task<ICollection<ContainerDTO>> ListContainerAsync(int id)
  {
    var containers = await containerService.ListContainerAsync(id);
    var dtos = containers.Select(c => c.ToDTO()).ToList();

    var states = await adapter.GetContainerState(
      dtos.Select(d => d.ContainerID).ToArray()
    );
    for (var i = 0; i < dtos.Count; i++)
    {
      dtos[i].State = states[i];
    }

    return dtos;
  }

  public async Task<ContainerDTO> PutContainerAsync(int id, ContainerDTO body)
  {
    var entry = await entryService.FindByIDAsync(body.EntryID);
    var image = await imageService.FindByIDAsync(body.ImageID);
    var container = await containerService.PutContainerAsync(
      body,
      image,
      entry
    );
    return container.ToDTO();
  }

  public async Task<EntryDTO?> FindDefaultEntryAsync(int appID)
  {
    var record = await appService.FindDefaultEntry(appID);
    if (record == null)
    {
      return null;
    }

    return await record.ToDTO().AddDeployInfo(record, adapter);
  }

  public Task PutDefaultEntryAsync(int appID, int entryID)
  {
    return Task.Run(() =>
    {
      appService.PutDefaultEntry(entryID);
    });
  }

  public async Task<ICollection<EntryMatcherDTO>> ListMatcherAsync(int id)
  {
    var query = from matchers in context.EntryMatchers
                where matchers.Entry.ID.Equals(id)
                select matchers;
    var records = await query.Include(e => e.Entry).ToArrayAsync();
    return records.Select(e => e.ToDTO()).ToArray();
  }

  public async Task<EntryMatcherDTO> PutMatcherAsync(int id, EntryMatcherDTO dto)
  {
    // query entry
    dto.EntryID = id;
    var query = from entry in context.Entries
                where entry.ID.Equals(id)
                select entry;
    var entryRecord = await query.FirstAsync();
    var record = dto.ToEntity(entryRecord);

    using var transcation = context.Database.BeginTransaction();
    // check current key exist
    var checkQuery = from matcher in context.EntryMatchers
                     where matcher.Entry.ID.Equals(id) && matcher.Key.Equals(dto.Key)
                     select matcher;
    if (checkQuery.Any())
    {
      throw new HttpStandardException(
        StatusCodes.Status400BadRequest,
        "current key already exist"
      );
    }

    context.EntryMatchers.Add(record);
    context.SaveChanges();
    transcation.Commit();

    return record.ToDTO();
  }

  public async Task DeleteMatcherAsync(int id, int matcherID)
  {
    await context.EntryMatchers.
      Where(e => e.ID.Equals(matcherID)).
      ExecuteDeleteAsync();
  }
}
