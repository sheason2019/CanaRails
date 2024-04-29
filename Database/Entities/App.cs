namespace CanaRails.Database.Entities;

public class App
{
  public int ID { get; set; }
  public required string Name { get; set; }
  public required string AppID { get; set; }
  public string Description { get; set; } = "";
  public Entry[] Entries { get; set; } = [];
  public Image[] Images { get; set; } = [];
  public AppMatch[] AppMatches { get; set; } = [];
}
