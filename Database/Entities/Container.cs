using System.ComponentModel.DataAnnotations.Schema;

namespace CanaRails.Database.Entities;

public class Container
{
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int ID { get; set; }
  public string ContainerID { get; set; } = "";
  public required int Port { get; set; }
  public required int Version { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
  public required Image Image { get; set; }
  public required Entry Entry { get; set; }
}
