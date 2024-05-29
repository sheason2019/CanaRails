namespace CanaRails.Database.Entities;

public enum PublishOrderStatus
{
  Pending, // 正在等待处理
  Approval, // 变更已完成
  Rejct, // 变更被拒绝
}

public class PublishOrder : Entity
{
  public required Entry Entry { get; set; }

  public required Image Image { get; set; }

  public int Port { get; set; }

  public int Replica { get; set; }

  public PublishOrderStatus Status { get; set; } = PublishOrderStatus.Pending;

  public DateTime? UpdatedAt { get; set; }
}
