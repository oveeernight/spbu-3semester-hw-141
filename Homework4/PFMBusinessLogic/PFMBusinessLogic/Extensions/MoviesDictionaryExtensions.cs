using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Extensions;

public static class MoviesDictionaryExtensions
{
    public static List<Movie> WithRelatedMovies(this List<Movie> source, 
        Dictionary<string, Person> personsWithMovies,
        Dictionary<string, Tag> tagsWithMovies)
    {
        Parallel.ForEach(source, movie =>
        {
            var candidates = movie.Actors.SelectMany(actor => personsWithMovies[actor.Name].Movies)
                .Union(movie.Directors.SelectMany(director => personsWithMovies[director.Name].Movies))
                .Union(movie.Tags.SelectMany(tag => tagsWithMovies[tag.Name].Movies)).ToArray();
            movie.Top10Related = candidates.OrderBy(movie.GetSimilarityRate).Take(10).ToList();
        });
        // foreach (var (_, movie) in source)
        // {
        //     var candidates = movie.Actors.SelectMany(actor => actor.Movies)
        //         .Union(movie.Tags.SelectMany(tag => tag.Movies)).ToArray();
        //     movie.Top10Related = candidates.OrderBy(m => movie.GetSimilarityRate(m)).Take(10).ToArray();
        // }

        return source;
    }

}