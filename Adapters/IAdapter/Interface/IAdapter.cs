namespace CanaRails.Adapters.IAdapter;

public interface IAdapter
{
  public IImageAdapter Image { get; }
  public IContainerAdapter Container { get; }
  public IOrderAdapter Order { get; }
}
