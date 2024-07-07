using Admin.Domains.Auth.Constants;
using Admin.Domains.Auth.Services;
using Admin.Domains.Core.Repositories;
using CanaRails.Controllers;
using CanaRails.Transformer;

namespace Admin.Apis;

public class ImageControllerImpl(
  ImageRepository imageFactory,
  AuthService authService
) : IImageController
{
  public Task<int> CountAsync(int appId)
  {
    return Task.FromResult(imageFactory.Count(appId));
  }

  public async Task<ImageDTO> CreateAsync(ImageDTO body)
  {
    await authService.RequireRole(Roles.Administrator);

    return imageFactory.Create(body).ToDTO();
  }

  public Task<ImageDTO> FindByIdAsync(int id)
  {
    return Task.FromResult(imageFactory.FindById(id).ToDTO());
  }

  public Task<ICollection<ImageDTO>> ListAsync(int appID)
  {
    var images = imageFactory.ListImageByAppId(appID);
    return Task.FromResult<ICollection<ImageDTO>>(
      images.Select(x => x.ToDTO()).ToArray()
    );
  }
}
