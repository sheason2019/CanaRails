using System.ComponentModel.DataAnnotations.Schema;

namespace CanaRails.Database.Entities;

public class AppMatcher
{
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int ID { get; set; }
  public required string Host { get; set; }
  public required App App { get; set; }
}
