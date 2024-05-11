using CanaRails.Adapters.DockerAdapter.Services;
using CanaRails.Database.Entities;


namespace CanaRails.Adapters.DockerAdapter;

public class DockerAdapter(DockerService service) : IAdapter.IAdapter
{
  public Task PullImage(Image image)
  {
    return service.CreateImageAsync(image);
  }
  public Task DeleteImage(Image image)
  {
    return service.DeleteImageAsync(image);
  }

  public Task<string> CreateContainer(Image image)
  {
    return service.CreateContainerAsync(image);
  }

  public Task StopContainer(string containerId)
  {
    return service.StopContainerAsync(containerId);
  }

  public Task RestartContainer(string containerId)
  {
    return service.RestartContainerAsync(containerId);
  }

  public Task RemoveContainer(string containerId)
  {
    return service.RemoveContainerAsync(containerId);
  }

  public Task<string[]> GetContainerState(string[] containerIds)
  {
    return service.GetContainerStateAsync(containerIds);
  }

  public Task<string> GetContainerIP(string containerId)
  {
    return service.GetContainerIPAsync(containerId);
  }
}
