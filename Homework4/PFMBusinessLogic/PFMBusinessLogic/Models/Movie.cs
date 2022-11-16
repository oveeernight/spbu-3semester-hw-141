using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace PFMBusinnecLogic;

public class Movie
{
    [Key]
    public string Title { get; set; }
    public  virtual ICollection<string> Actors { get; set; }
    public string? Director { get; set; }
    public virtual ICollection<string> Tags { get; set; }
    public string? Rate { get; set; }
}