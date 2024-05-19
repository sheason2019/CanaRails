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
      },
      new AuthConfig { },
      new Progress<JSONMessage>()
    );
  }

  public async Task DeleteImageAsync(Image image)
  {
    await client.Images.DeleteImageAsync(
      image.ImageName,
      new ImageDeleteParameters { }
    );
  }

  public async Task<string> CreateContainerAsync(Image image)
  {
    var container = await client.Containers.CreateContainerAsync(
      new CreateContainerParameters()
      {
        Image = image.ImageName,
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

  public async Task<string[]> GetContainerStateAsync(string[] containerIds)
  {
    var list = await client.Containers.ListContainersAsync(
      new ContainersListParameters
      {
        All = true
      }
    );
    var dict = new Dictionary<string, ContainerListResponse?>();
    foreach (var container in list)
    {
      dict.Add(container.ID, container);
    }
    return containerIds.
      Select(id => dict.GetValueOrDefault(id)?.State ?? "").
      ToArray();
  }

  public async Task<string> GetContainerIPAsync(string containerId)
  {
    var container = await client.Containers.InspectContainerAsync(containerId);
    return container.NetworkSettings.IPAddress;
  }
}
