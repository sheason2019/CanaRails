using Admin.Infrastructure.Repository.Entities;

namespace Admin.Domains.Core.Models.Entities;

public class App : Entity
{
  public required string Name { get; set; }
  public string Description { get; set; } = "";
  public List<string> Hostnames { get; set; } = [];
  public Entry? DefaultEntry { get; set; }
  public int? DefaultEntryId { get; set; }
  public ICollection<Entry> Entries { get; set; } = [];
  public ICollection<Image> Images { get; set; } = [];
}
