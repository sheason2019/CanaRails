namespace CanaRails.Database.Entities;

public class App : Entity
{
  public required string Name { get; set; }
  public string Description { get; set; } = "";
  public Entry? DefaultEntry { get; set; }
  public ICollection<Entry> Entries { get; set; } = [];
  public ICollection<Image> Images { get; set; } = [];
  public ICollection<AppMatcher> AppMatchers { get; set; } = [];
}
