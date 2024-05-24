namespace CanaRails.Database.Entities;

public class Entry : Entity
{
  public required string Name { get; set; }
  public string Description { get; set; } = "";
  public required App App { get; set; }
  public ICollection<EntryMatcher> EntryMatchers { get; set; } = [];
  public ICollection<PublishOrder> PublishOrders { get; set; } = [];
  public PublishOrder? CurrentPublishOrder { get; set; }
}
