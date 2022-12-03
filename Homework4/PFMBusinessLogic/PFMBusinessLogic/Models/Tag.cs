using System.ComponentModel.DataAnnotations;

namespace PFMBusinessLogic.Models;

public class Tag
{
    [Key]
    public string Name { get; set; }
    public virtual ICollection<Movie> Movies { get; set; }
}