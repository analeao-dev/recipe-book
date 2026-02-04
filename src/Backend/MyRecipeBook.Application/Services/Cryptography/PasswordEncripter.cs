using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.Cryptography;
public class PasswordEncripter
{
    private readonly string _addtionalKey;
    public PasswordEncripter(string addtionalKey) => _addtionalKey = addtionalKey;
    public string Encrypt(string password)
    {
        var key = "ABC";
        var newPassword = $"{password}{key}";

        var bytes = Encoding.UTF8.GetBytes(newPassword);
        var hashBytes = SHA512.HashData(bytes);

        return StringFromBytes(hashBytes);
    }

    //converte um array de bytes para string hexadecimal
    private static string StringFromBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }
}
