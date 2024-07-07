using Admin.Domains.Auth.Constants;
using Admin.Domains.Auth.Services;
using Admin.Domains.Core.Factories;
using Admin.Domains.Core.Repositories;
using Admin.Infrastructure.IDL;

namespace Admin.Apis;

public class ImageControllerImpl(
  ImageRepository imageFactory,
  DtoFactory dtoFactory,
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

    return dtoFactory.CreateImageDTO(imageFactory.Create(body));
  }

  public Task<ImageDTO> FindByIdAsync(int id)
  {
    return Task.FromResult(
      dtoFactory.CreateImageDTO(imageFactory.FindById(id))
    );
  }

  public Task<ICollection<ImageDTO>> ListAsync(int appID)
  {
    var images = imageFactory.ListImageByAppId(appID);
    return Task.FromResult<ICollection<ImageDTO>>(
      images.Select(dtoFactory.CreateImageDTO).ToArray()
    );
  }
}
