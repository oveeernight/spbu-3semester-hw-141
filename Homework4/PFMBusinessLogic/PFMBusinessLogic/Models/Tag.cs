using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using PFMBusinessLogic.Database.StorageElements;

namespace PFMBusinessLogic.Models;

public class Tag
{ 
    [Key] 
    public string Name { get; set; } = string.Empty;
    public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();

    public override bool Equals(object? obj)
    {
        if (obj is not Tag tag) return false;
        return Name == tag.Name;
    }

    public TagStorageElement ToStorageElement()
    {
        return new TagStorageElement
        {
            Name = Name,
            Movies = JsonSerializer.Serialize(Movies.SelectMany(m => m.Title))
        };
    }
}