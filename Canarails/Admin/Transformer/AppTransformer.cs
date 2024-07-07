using Admin.Domains.Core.Models.Entities;
using CanaRails.Controllers;

namespace CanaRails.Transformer;

public static class AppTransformer
{
  public static AppDTO ToDTO(this App app)
  {
    return new AppDTO
    {
      Id = app.ID,
      Name = app.Name,
      Description = app.Description,
      Hostnames = app.Hostnames,
      DefaultEntryId = app.DefaultEntryId,
    };
  }

  public static App ToEntity(this AppDTO dto)
  {
    return new App
    {
      ID = dto.Id,
      Name = dto.Name,
      Description = dto.Description,
      Hostnames = dto.Hostnames,
    };
  }
}