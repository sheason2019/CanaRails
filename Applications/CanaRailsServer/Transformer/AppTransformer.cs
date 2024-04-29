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
      AppID = app.AppID,
      Description = app.Description,
    };
  }

  public static App ToEntity(this AppDTO dto)
  {
    return new App
    {
      ID = dto.Id,
      Name = dto.Name,
      AppID = dto.AppID,
      Description = dto.Description,
    };
  }
}