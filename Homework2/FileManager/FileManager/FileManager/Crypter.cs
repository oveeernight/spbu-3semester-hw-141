using System.Security.Cryptography;
using System.Text;


namespace FileManager
{
    public static class Crypter
    {
        private static string? xmlString;
        public static string Encrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            xmlString = rsa.ToXmlString(true);
            var encryptContent = rsa.Encrypt(Encoding.UTF8.GetBytes(data), true);
            return Convert.ToBase64String(encryptContent);
        }
        
        public static string Decrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlString);
            var decryptContent = rsa.Decrypt(Convert.FromBase64String(data), true);
            return Encoding.UTF8.GetString(decryptContent);
        }
    }
}