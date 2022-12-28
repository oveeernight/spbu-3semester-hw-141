using System.ComponentModel.DataAnnotations;

namespace PFMBusinessLogic.Database.StorageElements;

public class MovieStorageElement
{
    [Key]
    public string Title { get; set; }
    public double Rate { get; set; }
    public string? Actors { get; set; }
    public string? Directors { get; set; }
    public string? Tags { get; set; }
    public string? Top10Related { get; set; }
}