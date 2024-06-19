using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;

namespace CanaRails.Services;

public class AuthService
{
  // 哈希化密码
  public (string salt, string passwordHash) HashPassword(string password)
  {
    var rand = new Random();
    // 生成 64 位随机 Salt 值
    var salt = new byte[64];
    rand.NextBytes(salt);

    // 使用 utf8 解码 Password
    var utf8 = new UTF8Encoding();
    var passwordBuf = utf8.GetBytes(password);

    // 创建加盐密码 Buffer
    var combineBuf = passwordBuf.Concat(salt).ToArray();

    // SHA256 计算哈希值
    var combineHash = SHA256.HashData(combineBuf);

    return (
      salt: Convert.ToBase64String(salt),
      passwordHash: Convert.ToBase64String(combineHash)
    );
  }
}