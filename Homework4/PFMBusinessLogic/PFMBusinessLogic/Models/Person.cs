using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using PFMBusinessLogic.Database.StorageElements;

namespace PFMBusinessLogic.Models;

public class Person
{
    [Key]
     public string Name { get; set; } = string.Empty;
     public  ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();

    public override bool Equals(object? obj)
    {
        if (obj is not Person person) return false;
        return person.Name == Name;
    }

    public PersonStorageElement ToStorageElement()
    {
        return new PersonStorageElement
        {
            Name = Name,
            // Movies = JsonSerializer.Serialize(Movies.Select(movie => movie.Title))
            Movies = JsonSerializer.Serialize(Movies.Select(movie => new Movie(){Title = movie.Title}))
        };
    }
}