using System.ComponentModel.DataAnnotations.Schema;

namespace CanaRails.Database.Entities;

public class Image
{
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int ID { get; set; }
  public required string Registry { get; set; }
  public required string ImageName { get; set; }
  public bool Ready { get; set; } = false;
  public required App App { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
  public ICollection<Container> Containers { get; set; } = [];
}
