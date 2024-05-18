using System.ComponentModel.DataAnnotations.Schema;

namespace CanaRails.Database.Entities;

public class Entry
{
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public required int ID { get; set; }
  public required string Name { get; set; }
  public string Description { get; set; } = "";
  public int Version { get; set; }
  public bool Default { get; set; }
  public required App App { get; set; }
  public ICollection<EntryMatcher> EntryMatchers { get; set; } = [];
  public ICollection<Container> Containers { get; set; } = [];
}
