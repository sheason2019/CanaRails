namespace CanaRails.Database.Entities;

public class Image
{
  public int ID { get; set; }
  public required string Registry { get; set; }
  public required string ImageName { get; set; }
  public required string TagName { get; set; }
  public required App App { get; set; }
  public Entry[] Entries { get; set; } = [];
}
