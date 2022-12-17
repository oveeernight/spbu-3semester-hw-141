using System.ComponentModel.DataAnnotations;

namespace PFMBusinessLogic.Models;

public class Person
{
    [Key] public string Name { get; set; } = string.Empty;
    public virtual ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
}