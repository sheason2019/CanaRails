using Docker.DotNet;
using Docker.DotNet.Models;

namespace CanaRails.Adapters.DockerAdapter.Services;

public class DockerService
{
  private readonly DockerClient client;
  public DockerService()
  {
    client = DockerConnectionService.CreateDockerClient();
  }

  public async Task CreateImageAsync(string imageName)
  {
    await client.Images.CreateImageAsync(
      new ImagesCreateParameters
      {
        FromImage = imageName,
        Tag = "latest",
      },
      new AuthConfig { },
      new Progress<JSONMessage>()
    );
  }

  public async Task<string> CreateContainerAsync(string imageName)
  {
    var container = await client.Containers.CreateContainerAsync(
      new CreateContainerParameters()
      {
        Image = imageName,
      }
    );
    await client.Containers.StartContainerAsync(
      container.ID,
      new ContainerStartParameters()
    );
    return container.ID;
  }
}