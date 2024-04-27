namespace CanaRails.Database.Entities;

public class Instance
{
  public required int ID { get; set; }
  public required string Registry { get; set; }
  public required string ImageName { get; set; }
  public required int Port { get; set; }
  public required App App { get; set; }
}
