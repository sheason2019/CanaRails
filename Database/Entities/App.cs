using System.ComponentModel.DataAnnotations.Schema;

namespace CanaRails.Database.Entities;

public class App
{
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int ID { get; set; }
  public required string Name { get; set; }
  public string Description { get; set; } = "";
  public ICollection<Entry> Entries { get; set; } = [];
  public ICollection<Image> Images { get; set; } = [];
  public ICollection<AppMatcher> AppMatchers { get; set; } = [];
}
