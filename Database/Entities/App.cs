namespace CanaRails.Database.Entities;

public class App
{
  public int ID { get; set; }
  public required string Name { get; set; }
  public string Description { get; set; } = "";
  public ICollection<Entry> Entries { get; set; } = [];
  public ICollection<Image> Images { get; set; } = [];
  public ICollection<AppMatch> AppMatches { get; set; } = [];
}
