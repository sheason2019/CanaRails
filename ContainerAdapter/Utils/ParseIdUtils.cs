using System.Text.RegularExpressions;

namespace CanaRails.ContainerAdapter.Utils;

public class ParseIdUtils
{
  public static int? ParseAppIdByHttpRouteName(string httpRouteName)
  {
    var pattern = @"^canarails\-http\-route\-by\-app\-(\d+)$";
    return Exec(httpRouteName, pattern);
  }

  public static int? ParseEntryIdByServiceName(string serviceName)
  {
    var pattern = @"^canarails\-service\-by\-entry\-(\d+)$";
    return Exec(serviceName, pattern);
  }

  public static int? ParseEntryIdByDeployName(string deployName)
  {
    var pattern = @"^canarails\-deployment\-by\-entry\-(\d+)$";
    return Exec(deployName, pattern);
  }

  private static int? Exec(string source, string pattern)
  {
    var match = Regex.Match(source, pattern);
    var matchId = match?.Groups[1].Value;
    if (matchId != null)
    {
      return int.Parse(matchId);
    }

    return null;
  }
}