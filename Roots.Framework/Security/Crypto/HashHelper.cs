using System.Security.Cryptography;
using System.Text;

namespace Roots.Framework.Security.Crypto;

public static class HashHelper
{
    public static string ComputeHash(string input)
    {
        using SHA256 sha256 = SHA256.Create();
        var combinedInput = input;
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedInput));

        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }
}