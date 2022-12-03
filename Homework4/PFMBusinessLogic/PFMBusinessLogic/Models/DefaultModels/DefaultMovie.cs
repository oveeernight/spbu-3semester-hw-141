namespace PFMBusinessLogic.DefaultModels;

/// <summary>
/// Represents movie which collection props are just string
/// </summary>
public class DefaultMovie
{
    public string Title { get; set; }
    public  virtual ICollection<string> Actors { get; set; }
    public string? Director { get; set; }
    public virtual ICollection<string> Tags { get; set; }
    public string? Rate { get; set; }
}