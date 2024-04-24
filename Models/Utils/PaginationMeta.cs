namespace CanaRails.Models.Utils;

public class PaginationMeta<T>
{
  public required T[] Data { get; set; }
  public int Total { get; set; } = 0;
}