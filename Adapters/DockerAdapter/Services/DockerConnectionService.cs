using Docker.DotNet;

namespace CanaRails.Adapters.DockerAdapter.Services;

public class DockerConnectionService
{
  public static DockerClient CreateDockerClient() { 
    return new DockerClientConfiguration().CreateClient();
  }
}