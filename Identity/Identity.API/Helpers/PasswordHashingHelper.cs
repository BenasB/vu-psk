using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace Identity.API.Helpers;

public class HashingHelper
{
    public static byte[] HashPassword(string input, string salt = "salt")
    {
        return Rfc2898DeriveBytes.Pbkdf2(Encoding.ASCII.GetBytes(input), Encoding.ASCII.GetBytes(salt), 5000, HashAlgorithmName.SHA512, 20);
    }
}
