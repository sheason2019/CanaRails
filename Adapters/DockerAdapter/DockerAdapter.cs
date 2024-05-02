using CanaRails.Adapters.DockerAdapter.Services;
using CanaRails.Database.Entities;
using CanaRails.Interfaces;

namespace CanaRails.Adapters.DockerAdapter;

public class DockerAdapter(DockerService service) : IAdapter
{
  public Task ApplyEntry(Entry entry)
  {
    throw new NotImplementedException();
  }
  public Task DeleteEntry(Entry entry)
  {
    throw new NotImplementedException();
  }

  public Task PullImage(Image image)
  {
    return service.CreateImageAsync(image);
  }
  public Task DeleteImage(Image image)
  {
    return service.DeleteImageAsync(image);
  }

}
