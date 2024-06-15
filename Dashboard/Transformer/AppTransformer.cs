using CanaRails.Controllers.App;
using CanaRails.Database.Entities;

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
      DefaultEntryId = app.DefaultEntry?.ID,
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