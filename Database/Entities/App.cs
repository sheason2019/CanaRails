namespace CanaRails.Database.Entities;

public class App
{
  public int ID { get; set; }
  public required string Name { get; set; }
  public required string Host { get; set; }
  public string Description { get; set; } = "";
  public Instance[] Instances { get; set; } = [];
}
