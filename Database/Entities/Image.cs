namespace CanaRails.Database.Entities;

public class Image : Entity
{
  public required string ImageName { get; set; }
  public required App App { get; set; }
  public ICollection<PublishOrder> PublishOrders { get; set; } = [];
}
