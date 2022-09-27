using System.Runtime.Serialization;


namespace FileManager
{
    [Serializable]
    internal class Settings : ISerializable
    {
        public string Password { get; set; }
        public string Login { get; set; }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}