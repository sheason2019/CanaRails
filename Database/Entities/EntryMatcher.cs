namespace CanaRails.Database.Entities;

public class EntryMatcher : Entity
{
  public required string Key { get; set; }
  public required string Value { get; set; }
  public required Entry Entry { get; set; }
}
