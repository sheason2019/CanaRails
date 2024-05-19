using CanaRails.Adapters.IAdapter;
using Docker.DotNet;


namespace CanaRails.Adapters.DockerAdapter;

public class DockerAdapter : IAdapter.IAdapter
{
  private readonly DockerClient client;
  private readonly IImageAdapter image;
  private readonly IContainerAdapter container;

  public DockerAdapter()
  {
    client = new DockerClientConfiguration().CreateClient();
    image = new ImageAdapter(client);
    container = new ContainerAdapter(client);
  }

  public IImageAdapter Image => image;

  public IContainerAdapter Container => container;
}
