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
      Registry = image.Registry,
      ImageName = image.ImageName,
      Ready = image.Ready,
      CreatedAt = ((DateTimeOffset)image.CreatedAt).ToUnixTimeMilliseconds(),
    };
  }

  public static Image ToEntity(this ImageDTO dto, App app)
  {
    return new Image
    {
      ID = dto.Id,
      Registry = dto.Registry,
      ImageName = dto.ImageName,
      App = app,
      Ready = dto.Ready,
      CreatedAt = DateTimeOffset.FromUnixTimeMilliseconds(dto.CreatedAt).DateTime,
    };
  }
}