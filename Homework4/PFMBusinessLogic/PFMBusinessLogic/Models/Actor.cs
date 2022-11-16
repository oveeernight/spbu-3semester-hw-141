using System.ComponentModel.DataAnnotations;

namespace PFMBusinnecLogic.Models;

public class Actor
{
    [Key] 
    public string Name { get; set; }
    public virtual ICollection<Movie> Movies { get; set; }
}