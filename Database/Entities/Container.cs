namespace CanaRails.Database.Entities;

public enum ContianerType
{
  Service, // 服务容器
  Ingress, // 网关容器
}

public class Container : Entity
{
  public required string ContainerID { get; set; }

  public required ContianerType ContainerType { get; set; }

  public required PublishOrder PublishOrder { get; set; }
}
