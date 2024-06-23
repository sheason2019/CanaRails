using System.Security.Cryptography;
using System.Text;

namespace CanaRails.Utils;

// 收集环境变量
public static class EnvVariables
{
  public static string? CANARAILS_ADMIN_PSWD
  {
    get { return Environment.GetEnvironmentVariable("CANARAILS_ADMIN_PSWD"); }
  }

  public static string? CANARAILS_DBHOST
  {
    get { return Environment.GetEnvironmentVariable("CANARAILS_DBHOST"); }
  }

  public static string? CANARAILS_DBUSER
  {
    get { return Environment.GetEnvironmentVariable("CANARAILS_DBUSER"); }
  }

  public static string? CANARAILS_DBPSWD
  {
    get { return Environment.GetEnvironmentVariable("CANARAILS_DBPSWD"); }
  }

  public static string? CANARAILS_DBNAME
  {
    get { return Environment.GetEnvironmentVariable("CANARAILS_DBNAME"); }
  }

  public static string? CANARAILS_CLIENT_CONFIG
  {
    get { return Environment.GetEnvironmentVariable("CANARAILS_CLIENT_CONFIG"); }
  }
}