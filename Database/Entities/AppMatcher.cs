namespace CanaRails.Database.Entities;

public class AppMatcher : Entity
{
  public required string Host { get; set; }
  public required App App { get; set; }
}
