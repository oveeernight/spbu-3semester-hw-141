using System.Collections;
using System.ComponentModel.DataAnnotations;
using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Models;

public class Movie
{
    [Key]
    public string Title { get; set; }
    public  virtual ICollection<Person> Actors { get; set; }
    public Person Director { get; set; }
    public virtual ICollection<Tag> Tags { get; set; }
    public string? Rate { get; set; }
}