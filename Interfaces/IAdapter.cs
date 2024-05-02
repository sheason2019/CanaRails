using CanaRails.Database.Entities;

namespace CanaRails.Interfaces;

public interface IAdapter
{
  public Task ApplyEntry(Entry entry);
  public Task DeleteEntry(Entry entry);
  public Task PullImage(Image image);
  public Task DeleteImage(Image image);
}
