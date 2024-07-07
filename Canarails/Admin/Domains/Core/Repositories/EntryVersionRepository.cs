using Admin.Domains.Core.Models.Entities;
using Admin.Infrastructure.Repository;
using Admin.Infrastructure.IDL;
using Microsoft.EntityFrameworkCore;

namespace Admin.Domains.Core.Repositories;

public class EntryVersionRepository(
  CanaRailsContext context,
  EntryRepository entryRepository,
  ImageRepository imageRepository
)
{
  public EntryVersion Create(EntryVersionDTO dto)
  {
    var entry = entryRepository.FindById(dto.EntryId);
    var image = imageRepository.FindById(dto.ImageId);

    var record = new EntryVersion
    {
      Port = dto.Port,
      Replica = dto.Replica,
      Entry = entry,
      Image = image,
    };

    context.EntryVersions.Add(record);
    context.SaveChanges();

    return record;
  }

  public EntryVersion[] ListByEntryId(int entryId)
  {
    var query = from evs in context.EntryVersions
                join entries in context.Entries on evs.Entry.ID equals entries.ID
                where entries.ID == entryId
                select evs;
    return [.. query
      .Include(e => e.Entry)
      .Include(e => e.Image)
    ];
  }
}
