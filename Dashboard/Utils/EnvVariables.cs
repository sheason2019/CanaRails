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

  public static byte[]? CANARAILS_AUTH_SECRET_BYTES
  {
    get
    {
      var secret = Environment.GetEnvironmentVariable("CANARAILS_AUTH_SECRET");
      if (secret == null) return null;

      var secretByte = Encoding.UTF8.GetBytes(secret);
      return SHA256.HashData(secretByte);
    }
  }
}