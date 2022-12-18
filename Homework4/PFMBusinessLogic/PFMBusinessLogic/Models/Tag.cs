using System.ComponentModel.DataAnnotations;

namespace PFMBusinessLogic.Models;

public class Tag
{
    [Key]
    public string Name { get; set; }
    public virtual ICollection<Movie> Movies { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Tag tag) return false;
        return Name == tag.Name;
    }
}