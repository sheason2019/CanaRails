using CanaRails.Database.Entities;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace CanaRails.Adapters.DockerAdapter.Services;

public class DockerService
{
  private readonly DockerClient client;
  public DockerService()
  {
    client = new DockerClientConfiguration().CreateClient();
  }

  public async Task CreateImageAsync(Image image)
  {
    await client.Images.CreateImageAsync(
      new ImagesCreateParameters
      {
        FromImage = image.ImageName,
        Tag = image.TagName,
      },
      new AuthConfig { },
      new Progress<JSONMessage>()
    );
  }

  public async Task<string> CreateContainerAsync(Image image)
  {
    var container = await client.Containers.CreateContainerAsync(
      new CreateContainerParameters()
      {
        Image = $"{image.ImageName}:{image.TagName}",
      }
    );
    await RestartContainerAsync(container.ID);
    return container.ID;
  }

  public async Task RestartContainerAsync(string containerId)
  {
    await client.Containers.RestartContainerAsync(
      containerId,
      new ContainerRestartParameters()
    );
  }

  public async Task StopContainerAsync(string containerId)
  {
    await client.Containers.StopContainerAsync(
      containerId,
      new ContainerStopParameters()
    );
  }

  public async Task RemoveContainerAsync(string containerId)
  {
    await client.Containers.RemoveContainerAsync(
      containerId,
      new ContainerRemoveParameters()
    );
  }
}