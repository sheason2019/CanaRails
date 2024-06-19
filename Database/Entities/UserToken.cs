namespace CanaRails.Database.Entities;

public class UserToken : Entity
{
  public required User User { get; set; }

  public required string TokenString { get; set; }
}
