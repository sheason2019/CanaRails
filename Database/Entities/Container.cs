namespace CanaRails.Database.Entities;

public class Container
{
  public int ID { get; set; }
  public string ContainerID { get; set; } = "";
  public required int Port { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public required Image Image { get; set; }
  public required Entry Entry { get; set; }
}
