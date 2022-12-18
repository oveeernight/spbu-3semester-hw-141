using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Models;

public class Movie
{
    [Key] public string Title { get; set; } = string.Empty;

    public virtual ICollection<Person> Actors { get; set; } = new HashSet<Person>();
    public Person Director { get; set; } = new ();
    public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
    public string? Rate { get; set; }
    public virtual ICollection<Movie> Top10Related { get; set; }

    public double GetSimilarityRate(Movie movie)
    {
        var tagsSimilarity =  Tags.Intersect(movie.Tags).Count() / (double)Math.Max(Tags.Count, movie.Tags.Count);
        var personsSimilarity = Actors
                                    .Union(new[] { Director })
                                    .Intersect(movie.Actors.Union(new[] { movie.Director }))
                                    .Count() /
                                ((double)Math.Max(Actors.Count, movie.Actors.Count) + 1);
        var firstSimilarity = Math.Max(tagsSimilarity + personsSimilarity, 0.5);
        var secondSimilarity = movie.Rate != null ? double.Parse(movie.Rate, CultureInfo.InvariantCulture) / 20 : 0;
        return firstSimilarity + secondSimilarity;
    }
}