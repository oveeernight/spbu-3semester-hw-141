namespace Linq;

public class Record
{
    public User Author { get; set; }
    public string Message { get; set; }

    public Record(User author, String message)
    {
        Author = author;
        Message = message;
    }
}