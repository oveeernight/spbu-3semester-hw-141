using System.Runtime.Serialization;

namespace FileManager
{
    [Serializable]
    public class AuthCredits
    {
        public string Login { get; set; }
        public string Password { get; set; }

        [OnSerializing]
        public void OnSerializing()
        {
            Login = Crypter.Encrypt(Login);
            Password = Crypter.Encrypt(Password);
        }

        [OnDeserialized]
        public void OnDeserialized()
        {
            Login = Crypter.Decrypt(Login);
            Password = Crypter.Decrypt(Password);
        }
    }
}