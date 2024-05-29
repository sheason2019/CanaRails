using CanaRails.Controllers.Image;
using CanaRails.Database.Entities;

namespace CanaRails.Transformer;

public static class ImageTransformer
{
  public static ImageDTO ToDTO(this Image image)
  {
    return new ImageDTO
    {
      Id = image.ID,
      AppId = image.App.ID,
      ImageName = image.ImageName,
      CreatedAt = ((DateTimeOffset)image.CreatedAt).ToUnixTimeMilliseconds(),
    };
  }

  public static Image ToEntity(this ImageDTO dto, App app)
  {
    return new Image
    {
      ID = dto.Id,
      ImageName = dto.ImageName,
      App = app,
      CreatedAt = DateTimeOffset.FromUnixTimeMilliseconds(dto.CreatedAt).DateTime,
    };
  }
}