using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Extensions;

public static class MoviesDictionaryExtensions
{
    public static Dictionary<string, Movie> WithFullTagsAndPersons(this Dictionary<string, Movie> source,
        Dictionary<string, Person> persons,
        Dictionary<string, Tag> tags)
    {
        foreach (var (_, movie) in source)
        {
            movie.Actors = movie.Actors.Select(actor => persons[actor.Name]).ToHashSet();
            movie.Tags = movie.Tags.Select(tag => tags[tag.Name]).ToHashSet();
            movie.Director = persons.ContainsKey(movie.Director.Name) ? persons[movie.Director.Name] : new Person();
        }

        return source;
    }

    public static Dictionary<string, Movie> WithRelatedMovies(this Dictionary<string, Movie> source)
    {
        foreach (var (_, movie) in source)
        {
            var candidates = movie.Actors.SelectMany(actor => actor.Movies)
                .Union(movie.Tags.SelectMany(tag => tag.Movies)).ToArray();
            movie.Top10Related = candidates.OrderBy(m => movie.GetSimilarityRate(m)).Take(10).ToArray();
        }

        return source;
    }

}