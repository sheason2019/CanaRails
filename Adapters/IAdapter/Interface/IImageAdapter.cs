using CanaRails.Adapters.IAdapter.Models;
using CanaRails.Database.Entities;

namespace CanaRails.Adapters.IAdapter;

public interface IImageAdapter
{
  public Task<ImageInfo[]> GetInfo(Image[] images);
  public Task Pull(Image image);
  public Task Delete(Image image);
  public Task<ContainerInfo[]> Apply(Image image, int replica);
}
