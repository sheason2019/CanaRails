namespace CanaRails.Database.Entities;

public class Entry
{
  public required int ID { get; set; }
  public required string Name { get; set; }
  public string Description { get; set; } = "";
  public required int Port { get; set; }
  public required App App { get; set; }
  public ICollection<EntryMatch> EntryMatches { get; set; } = [];
  public required Image Image { get; set; }
}
