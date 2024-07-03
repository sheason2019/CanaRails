using System.Text.RegularExpressions;

namespace Admin.Domains.Core.Factories;

public class ResourceNameFactory
{
  public string GetName(int id)
  {
    return $"canarails-resource-{id}";
  }

  public int? GetId(string name)
  {
    var pattern = @"^canarails\-resource\-(\d+)$";
    var match = Regex.Match(name, pattern);
    var matchId = match?.Groups[1].Value;

    // 尝试解析匹配 id，若解析失败则返回 null
    try
    {
      if (matchId != null) return int.Parse(matchId);
    }
    catch { }

    return null;
  }
}
