namespace CanaRails.Database.Entities;

public class Entry
{
  public required int ID { get; set; }
  public required string Name { get; set; }
  public string Description { get; set; } = "";
  public required App App { get; set; }
  public ICollection<EntryMatcher> EntryMatchers { get; set; } = [];
  public ICollection<Container> Containers { get; set; } = [];
}
