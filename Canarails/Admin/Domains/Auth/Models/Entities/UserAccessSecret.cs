using Admin.Infrastructure.Repository.Entities;
using Microsoft.AspNetCore.Identity;

namespace Admin.Domains.Auth.Models.Entities;

public class UserAccessSecret : Entity
{
  public required IdentityUser User { get; set; }

  public required string Token { get; set; }

  public required string Title { get; set; }

  public string Description { get; set; } = "";

  public required DateTime ExpiredAt { get; set; }

  public required DateTime ActivedAt { get; set; }
}
