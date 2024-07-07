using Admin.Infrastructure.Repository.Entities;

namespace Admin.Domains.Core.Models.Entities;

public class EntryVersion : Entity
{
  public required Entry Entry { get; set; }

  public required Image Image { get; set; }

  public required int Port { get; set; }

  public required int Replica { get; set; }
}
