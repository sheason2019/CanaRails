namespace CanaRails.Database.Entities;

public class User : Entity
{
  public required string Username { get; set; }

  public required string PasswordHash { get; set; }

  public required string PasswordSalt { get; set; }

  public UserToken[] UserTokens { get; set; } = [];
}
