using System.Net;
using CanaRails.Controllers.Entry;
using CanaRails.Database;
using CanaRails.Exceptions;
using CanaRails.Transformer;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Services;

public class EntryService(
  CanaRailsContext context,
  AppService appService,
  ImageService imageService
)
{
  public async Task<Database.Entities.Entry> CreateEntry(EntryDTO dto)
  {
    using var transcation = context.Database.BeginTransaction();
    var count = await context.Entries.
      Where(e => e.App.ID.Equals(dto.AppID) && e.Name.Equals(dto.Name)).
      CountAsync();
    if (count > 0) {
      throw new HttpStandardException(400, "该应用下已存在同名应用入口");
    }

    var app = await appService.FindByIDAsync(dto.AppID);
    var image = await imageService.FindByIDAsync(dto.ImageID);

    var entry = dto.ToEntity(app, image);
    context.Entries.Add(entry);
    await context.SaveChangesAsync();
    await transcation.CommitAsync();
    return entry;
  }
}