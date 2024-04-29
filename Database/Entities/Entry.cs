namespace CanaRails.Database.Entities;

public class Entry
{
  public required int ID { get; set; }
  public required string Name { get; set; }
  public string Description { get; set; } = "";
  public required int Port { get; set; }
  public required App App { get; set; }
  public EntryMatch[] EntryMatches { get; set; } = [];
  public Image? CurrentImage { get; set; }
  public string? CurrentContainerID { get; set; }
}
