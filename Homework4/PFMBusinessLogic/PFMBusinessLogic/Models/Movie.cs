using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Mime;
using System.Text.Json;
using PFMBusinessLogic.Database.StorageElements;
using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Models;

public class Movie
{
     [Key] 
     public string Title { get; set; } = string.Empty;

    public  ICollection<Person> Actors { get; set; } = new HashSet<Person>();
    public ICollection<Person> Directors { get; set; } = new HashSet<Person>();
    public  ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
    public double Rate { get; set; }
    public  ICollection<Movie> Top10Related { get; set; }

    public double GetSimilarityRate(Movie movie)
    {
        var tagsSimilarity =  Tags.Intersect(movie.Tags).Count() / (double)Math.Max(Tags.Count, movie.Tags.Count);
        var personsSimilarity = Actors
                                    .Intersect(movie.Actors)
                                    .Count() /
                                ((double)Math.Max(Actors.Count, movie.Actors.Count) + 1);
        var firstSimilarity = Math.Max(tagsSimilarity + personsSimilarity, 0.5);
        var secondSimilarity = movie.Rate / 20;
        return firstSimilarity + secondSimilarity;
    }

    public MovieStorageElement ToStorageElement()
    {
        var actors = JsonSerializer.Serialize(Actors.Select(actor => new Person{Name = actor.Name}));
        var directors = JsonSerializer.Serialize(Directors.Select(director =>  new Person{Name = director.Name}));
        var related = JsonSerializer.Serialize(Top10Related.Select(m => new Movie{Title = m.Title}));
        var tags = JsonSerializer.Serialize(Tags.Select(tag => new Tag{Name = tag.Name}));
        return new MovieStorageElement
        {
            Title = Title,
            Actors = actors,
            Directors = directors,
            Tags = tags,
            Top10Related = related,
            Rate = Rate
        };
    }
}