using System.ComponentModel.DataAnnotations.Schema;

namespace CanaRails.Database.Entities;

public class EntryMatcher
{
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int ID { get; set; }
  public required string Key { get; set; }
  public required string Value { get; set; }
  public required Entry Entry { get; set; }
}
