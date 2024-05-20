using CanaRails.Adapters.IAdapter;
using CanaRails.Adapters.IAdapter.Models;
using CanaRails.Database.Entities;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace CanaRails.Adapters.DockerAdapter;

public class ImageAdapter(DockerClient client) : IImageAdapter
{
  public async Task<ContainerInfo[]> Apply(Image image, int replica)
  {
    var infoArray = new ContainerInfo[replica];
    for (var i = 0; i < replica; i++)
    {
      var container = await client.Containers.CreateContainerAsync(
        new CreateContainerParameters
        {
          Image = image.ImageName,
        },
        CancellationToken.None
      );
      await client.Containers.StartContainerAsync(
        container.ID,
        new ContainerStartParameters { },
        CancellationToken.None
      );
      infoArray[i] = new ContainerInfo
      {
        ContainerId = container.ID,
      };
    }
    return infoArray;
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