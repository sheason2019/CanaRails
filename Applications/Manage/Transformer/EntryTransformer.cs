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
      Port = entry.Port,
      Description = entry.Description,
      AppID = entry.App.ID,
      ImageID = entry.Image.ID,
    };
  }

  public static Entry ToEntity(this EntryDTO dto, App app, Image image)
  {
    return new Entry
    {
      ID = dto.Id,
      Name = dto.Name,
      Port = dto.Port,
      Description = dto.Description,
      App = app,
      Image = image,
    };
  }
}