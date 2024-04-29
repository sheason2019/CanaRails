namespace CanaRails.Database.Entities;

public class AppMatch
{
  public int ID { get; set; }
  public required string Host { get; set; }
  public required App App { get; set; }
}
