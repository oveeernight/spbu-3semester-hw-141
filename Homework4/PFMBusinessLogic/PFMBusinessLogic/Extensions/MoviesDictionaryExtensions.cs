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

}