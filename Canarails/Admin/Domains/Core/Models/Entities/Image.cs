using Admin.Infrastructure.Repository.Entities;

namespace Admin.Domains.Core.Models.Entities;

public class Image : Entity
{
  public required string ImageName { get; set; }
  public required App App { get; set; }
  public ICollection<EntryVersion> EntryVersions { get; set; } = [];
}
