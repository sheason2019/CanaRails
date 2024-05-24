namespace CanaRails.Database.Entities;

public enum PublishOrderStatus
{
  Created, // 容器发布工单已创建
  Running, // 正在执行变更
  Done, // 变更已完成
}

public class PublishOrder : Entity
{
  public required Entry Entry { get; set; }

  public required Image Image { get; set; }

  public int Port { get; set; }

  public int Replica { get; set; }

  public ICollection<Container> Containers { get; set; } = [];

  public PublishOrderStatus Status { get; set; } = PublishOrderStatus.Created;

  public DateTime? CompleteAt { get; set; }
}
