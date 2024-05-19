using CanaRails.Adapters.IAdapter;
using CanaRails.Adapters.IAdapter.Models;
using CanaRails.Database.Entities;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace CanaRails.Adapters.DockerAdapter;

public class ImageAdapter(DockerClient client) : IImageAdapter
{
  public Task<ContainerInfo[]> Apply(Image image)
  {
    throw new NotImplementedException();
  }

  public Task Delete(Image image)
  {
    throw new NotImplementedException();
  }

  public Task<ImageInfo[]> GetInfo(Image[] images)
  {
    throw new NotImplementedException();
  }

  public async Task Pull(Image image)
  {
    await client.Images.CreateImageAsync(
      new ImagesCreateParameters
      {
        FromImage = image.ImageName,
      },
      new AuthConfig { },
      new Progress<JSONMessage> { }
    );
  }
}