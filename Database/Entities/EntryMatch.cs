namespace CanaRails.Database.Entities;

public class EntryMatch
{
  public int ID { get; set; }
  public required string Key { get; set; }
  public required string Value { get; set; }
  public required Entry Entry { get; set; }
}
