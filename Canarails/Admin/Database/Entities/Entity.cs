using System.ComponentModel.DataAnnotations.Schema;

namespace CanaRails.Database.Entities;

public class Entity
{
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int ID { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
}
