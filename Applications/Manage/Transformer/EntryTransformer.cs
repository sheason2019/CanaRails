using CanaRails.Controllers.Entry;
using CanaRails.Database.Entities;

namespace CanaRails.Transformer;

public static class EntryTransformer
{
  public static EntryDTO ToDTO(this Entry entry)
  {
    return new EntryDTO
    {
      Id = entry.ID,
      Name = entry.Name,
      Description = entry.Description,
      AppID = entry.App.ID,
    };
  }

  public static Entry ToEntity(this EntryDTO dto, App app)
  {
    return new Entry
    {
      ID = dto.Id,
      Name = dto.Name,
      Description = dto.Description,
      App = app,
    };
  }
}