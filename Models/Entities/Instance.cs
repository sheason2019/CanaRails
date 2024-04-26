namespace CanaRails.Models.Entities;

public class Instance
{
  public required int ID { get; set; }
  public required string ImageName { get; set; }
  public required App App { get; set; }
}
