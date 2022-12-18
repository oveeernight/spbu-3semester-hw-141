using System.ComponentModel.DataAnnotations;

namespace PFMBusinessLogic.Models;

public class Person
{
    [Key] public string Name { get; set; } = string.Empty;
    public virtual ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();

    public override bool Equals(object? obj)
    {
        if (obj is not Person person) return false;
        return person.Name == Name;
    }
}