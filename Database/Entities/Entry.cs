namespace CanaRails.Database.Entities;

public class Entry : Entity
{
  public required string Name { get; set; }
  public string Description { get; set; } = "";
  public required App App { get; set; }
  public List<EntryMatcher> EntryMatchers { get; set; } = [];
  public ICollection<PublishOrder> PublishOrders { get; set; } = [];
}

public class EntryMatcher
{
  public required string Key { get; set; }
  public required string Value { get; set; }
}
