namespace PFMBusinnecLogic;

public class Movie
{
    public string Title { get; set; }
    public HashSet<string> Actors { get; set; }
    public string? Director { get; set; }
    public HashSet<string> Tags { get; set; }
    public string? Rate { get; set; }
}