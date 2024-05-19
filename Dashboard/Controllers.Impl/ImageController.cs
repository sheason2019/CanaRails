using CanaRails.Controllers.Image;
using CanaRails.Services;
using CanaRails.Transformer;

namespace CanaRails.Controllers.Impl;

public class ImageControllerImpl(ImageService service) : IImageController
{
  public Task<int> CountAsync(int appID)
  {
    return service.CountAsync(appID);
  }

  public async Task<ImageDTO> CreateAsync(ImageDTO body)
  {
    var image = await service.CreateImageAsync(body);
    return image.ToDTO();
  }

  public Task<ImageDTO> FindByIdAsync(int id)
  {
    throw new NotImplementedException();
  }

    public async Task<ICollection<ImageDTO>> ListAsync(int appID)
  {
    var images = await service.ListImageByAppIDAsync(appID);
    return images.Select(x => x.ToDTO()).ToArray();
  }
}
