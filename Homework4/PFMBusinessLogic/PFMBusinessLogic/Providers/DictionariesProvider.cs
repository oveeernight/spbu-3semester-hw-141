using System.Globalization;
using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Providers;

/// <summary>
/// Provides required dictionaries with strings instead of models
/// </summary>
public static class DictionariesProvider
{
    public static List<Movie> GetMovies(Dictionary<string, string> codesToTitles,
        Dictionary<string, string> codesToRatings,
        Dictionary<string, List<string>> movieLensCodesToTagIds,
        Dictionary<string, string> tagCodesToTitles,
        Dictionary<string, List<string>> codesToActorIds,
        Dictionary<string, List<string>> codesToDirectorIds,
        Dictionary<string, string> imdbToMovieLensIds,
        Dictionary<string,(string, List<string>)> personIdsToNameAndStarredFilms)
    {
        Console.WriteLine("Creating movies storage elements");
        var result = new Dictionary<string, Movie>();
        Parallel.ForEach(codesToTitles, pair =>
        {
            var code = pair.Key;
            var actorIds = codesToActorIds.ContainsKey(code) ? codesToActorIds[code] : new List<string>();
            var actors = new HashSet<Person>();
            foreach (var actorId in actorIds.Where(personIdsToNameAndStarredFilms.ContainsKey))
            {
                actors.Add(new Person() { Name = personIdsToNameAndStarredFilms[actorId].Item1 });
            }

            var directorIds = codesToDirectorIds.ContainsKey(code) ? codesToDirectorIds[code] : new List<string>();
            var directors = new HashSet<Person>();
            foreach (var directorId in directorIds.Where(personIdsToNameAndStarredFilms.ContainsKey))
            {
                directors.Add(new Person() { Name = personIdsToNameAndStarredFilms[directorId].Item1 });
            }



            var tagIds =
                imdbToMovieLensIds.ContainsKey(code) && movieLensCodesToTagIds.ContainsKey(imdbToMovieLensIds[code])
                    ? movieLensCodesToTagIds[imdbToMovieLensIds[code]]
                    : new List<string>();
            var tags = new HashSet<Tag>();
            foreach (var tagCode in tagIds)
            {
                tags.Add(new Tag() { Name = tagCodesToTitles[tagCode] });
            }

            var movie = new Movie
            {
                Title = codesToTitles[code],
                Rate = codesToRatings.ContainsKey(code)
                    ? double.Parse(codesToRatings[code], CultureInfo.InvariantCulture)
                    : 0,
                Directors = directors,
                Actors = actors,
                Tags = tags.ToHashSet()
            };

            lock (result)
            {
                if (!result.ContainsKey(pair.Value))
                    result.Add(pair.Value, movie);
            }
        });
        
        return result.Values.ToList();
    }

    public static Dictionary<string, Tag> GetTags(List<Movie> movies)
    {
        var result = new Dictionary<string, Tag>();
        foreach (var movie in movies)
        {
            foreach (var tag in movie.Tags)
            {
                if (result.ContainsKey(tag.Name))
                {
                    result[tag.Name].Movies.Add(movie);
                }
                else
                {
                    result.Add(tag.Name, new Tag{Name = tag.Name, Movies = new List<Movie>{movie}});
                }
            }
        }

        return result;
    }
    
    public static Dictionary<string, Person> GetPersons(List<Movie> movies)
    {
        var result = new Dictionary<string, Person>();
        foreach (var movie in movies)
        {
            foreach (var person in movie.Actors.Union(movie.Directors))
            {
                if (result.ContainsKey(person.Name))
                {
                    result[person.Name].Movies.Add(movie);
                }
                else
                {
                    result.Add(person.Name, new Person() {Name = person.Name, Movies = new List<Movie>{movie}});
                }
            }
        }

        return result;
    }
}