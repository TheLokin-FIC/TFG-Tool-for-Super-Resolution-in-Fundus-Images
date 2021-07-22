using System;
using System.Security.Cryptography;
using System.Text;

namespace Business.Utils
{
    public static class Encrypter
    {
        public static string Crypt(string key)
        {
            HashAlgorithm hashAlgorithm = new SHA256Managed();

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] encryptedkeyBytes = hashAlgorithm.ComputeHash(keyBytes);

            return Convert.ToBase64String(encryptedkeyBytes);
        }
    }
}