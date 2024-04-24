namespace CanaRails.Models.Entities;

public class App
{
  public int ID { get; set; }
  public required string Name { get; set; }
  public required string Registry { get; set; }
  public Instance[] Instances { get; set; } = [];
}
