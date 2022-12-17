using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Providers;

/// <summary>
/// Provides required dictionaries with strings instead of models
/// </summary>
public static class DictionariesProvider
{
    public static Dictionary<string, Movie> GetMovies(Dictionary<string, string> codesToTitles,
        Dictionary<string, string> codesToRatings,
        Dictionary<string, List<string>> movieLensCodesToTagIds,
        Dictionary<string, string> tagCodesToTitles,
        Dictionary<string, List<string>> codesToActorIds,
        Dictionary<string, string> codesToDirectorIds,
        Dictionary<string, string> imdbToMovieLensIds,
        Dictionary<string,(string, List<string>)> personIdsToNameAndStarredFilms)
    {
        var result = new Dictionary<string, Movie>();
        foreach (var (code, value) in codesToTitles)
        {
            var movie = CreateMovie(code, codesToTitles: codesToTitles,
                codesToActorIds: codesToActorIds,
                codesToDirectorIds: codesToDirectorIds,
                codesToRatings: codesToRatings,
                tagCodesToTitles: tagCodesToTitles,
                imdbToMovieLensIds: imdbToMovieLensIds,
                movieLensCodesToTagIds: movieLensCodesToTagIds,
                personIdsToNameAndStarredFilms: personIdsToNameAndStarredFilms
                );
            if (!result.ContainsKey(value))
                 result.Add(value, movie);
        }

        return result;
    }
    
    public static Dictionary<string, Person> GetPersons(
        Dictionary<string, Movie> movies,
        Dictionary<string, (string, List<string>)> personIdsToNameAndStarredFilms,
        Dictionary<string, string> codesToTitles)
    {
        var result = new Dictionary<string, Person>();
        foreach (var (_, (name, starredMovieIds)) in personIdsToNameAndStarredFilms)
        {
            var actorMovies = new HashSet<Movie>();
            foreach (var movie in starredMovieIds.Select(id =>
                         codesToTitles.ContainsKey(id) && movies.ContainsKey(codesToTitles[id])
                             ? movies[codesToTitles[id]]
                             : new Movie()))
            {
                actorMovies.Add(movie);
            }

            if (!result.ContainsKey(name))
                result.Add(name, new Person
                {
                    Movies = actorMovies, Name = name
                });
        }
        
        return result;
    }
    
    public static Dictionary<string, Tag> GetTags(Dictionary<string, Movie> movies)
    {
        var result = new Dictionary<string, Tag>();
        foreach (var (_, movie) in movies)
        {
            foreach (var tag in movie.Tags)
            {
                if (result.ContainsKey(tag.Name))
                {
                    result[tag.Name].Movies.Add(movie);
                }
                else
                {
                    result.Add(tag.Name, new Tag
                    {
                        Name = tag.Name, 
                        Movies = new HashSet<Movie>(){movie}
                    });
                }
            }
        }

        return result;
    }
    
    private static Movie CreateMovie(string code,
        IReadOnlyDictionary<string, string> codesToTitles,
        IReadOnlyDictionary<string, string> codesToRatings,
        IReadOnlyDictionary<string, List<string>> movieLensCodesToTagIds,
        IReadOnlyDictionary<string, string> tagCodesToTitles,
        IReadOnlyDictionary<string, List<string>> codesToActorIds,
        IReadOnlyDictionary<string, string> codesToDirectorIds,
        IReadOnlyDictionary<string, string> imdbToMovieLensIds,
        IReadOnlyDictionary<string,(string, List<string>)> personIdsToNameAndStarredFilms)
    {
        var actorIds =  codesToActorIds.ContainsKey(code) ? codesToActorIds[code] : new List<string>();
        var actors = new HashSet<Person>();
        foreach (var actorId in actorIds.Where(personIdsToNameAndStarredFilms.ContainsKey))
        {
            actors.Add(new Person() { Name = personIdsToNameAndStarredFilms[actorId].Item1 });
        }

        
        var tagIds = imdbToMovieLensIds.ContainsKey(code) && movieLensCodesToTagIds.ContainsKey(imdbToMovieLensIds[code]) ?
            movieLensCodesToTagIds[imdbToMovieLensIds[code]] : new List<string>();
        var tags = new HashSet<Tag>();
        foreach (var tagCode in tagIds)
        {
            tags.Add(new Tag() { Name = tagCodesToTitles[tagCode]});
        }
        
        return new Movie()
        {
            Title = codesToTitles[code],
            Rate = codesToRatings.ContainsKey(code) ? codesToRatings[code] : null,
            Director = codesToDirectorIds.ContainsKey(code) ? 
                new Person()
                 { Name = personIdsToNameAndStarredFilms[codesToDirectorIds[code]].Item1 } 
                : new Person(),
            Actors = actors,
            Tags = tags.ToHashSet()
        };
    }
}