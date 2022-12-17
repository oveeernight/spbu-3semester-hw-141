using PFMBusinessLogic.DefaultModels;

namespace PFMBusinessLogic.Providers;

/// <summary>
/// Provides required dictionaries with strings instead of models
/// </summary>
public static class DefaultDictionariesProvider
{
    public static Dictionary<string, DefaultMovie> GetMovies(Dictionary<string, string> codesToTitles,
        Dictionary<string, string> codesToRatings,
        Dictionary<string, List<string>> movieLensCodesToTagIds,
        Dictionary<string, string> tagCodesToTitles,
        Dictionary<string, List<string>> codesToActorIds,
        Dictionary<string, string> codesToDirectorIds,
        Dictionary<string, string> imdbToMovieLensIds,
        Dictionary<string,(string, List<string>)> actorIdsToNameAndStarredFilms)
    {
        var result = new Dictionary<string, DefaultMovie>();
        foreach (var (code, value) in codesToTitles)
        {
            var DefaultMovie = GetMovieById(code, codesToTitles: codesToTitles,
                codesToActorIds: codesToActorIds,
                codesToDirectorIds: codesToDirectorIds,
                codesToRatings: codesToRatings,
                tagCodesToTitles: tagCodesToTitles,
                imdbToMovieLensIds: imdbToMovieLensIds,
                movieLensCodesToTagIds: movieLensCodesToTagIds,
                actorIdsToNameAndStarredFilms: actorIdsToNameAndStarredFilms
                );
            if (!result.ContainsKey(value))
                 result.Add(value, DefaultMovie);
        }

        return result;
    }
    
    public static Dictionary<string, HashSet<string>> GetActorsAndDirectors(
        Dictionary<string, DefaultMovie> movies,
        Dictionary<string, (string, List<string>)> actorIdsToNameAndStarredFilms,
        Dictionary<string, string> codesToTitles)
    {
        var result = new Dictionary<string, HashSet<string>>();
        foreach (var (_, (name, starredFilmIds)) in actorIdsToNameAndStarredFilms)
        {
            var actorMovies = new HashSet<string>();
            foreach (var defaultMovie in starredFilmIds.Select(filmId =>
                         codesToTitles.ContainsKey(filmId) && movies.ContainsKey(codesToTitles[filmId])
                             ? movies[codesToTitles[filmId]]
                             : new DefaultMovie()))
            {
                actorMovies.Add(defaultMovie.Title);
            }

            if (!result.ContainsKey(name))
                result.Add(name, actorMovies);
        }

        return result;
    }
    
    public static Dictionary<string, HashSet<string>> GetTags(Dictionary<string, DefaultMovie> movies)
    {
        var result = new Dictionary<string, HashSet<string>>();
        foreach (var (title, movie) in movies)
        {
            foreach (var tag in movie.Tags)
            {
                if (result.ContainsKey(tag))
                {
                    result[tag].Add(title);
                }
                else
                {
                    result.Add(tag, new HashSet<string> { title });
                }
            }
        }

        return result;
    }
    
    private static DefaultMovie GetMovieById(string code,
        IReadOnlyDictionary<string, string> codesToTitles,
        IReadOnlyDictionary<string, string> codesToRatings,
        IReadOnlyDictionary<string, List<string>> movieLensCodesToTagIds,
        IReadOnlyDictionary<string, string> tagCodesToTitles,
        IReadOnlyDictionary<string, List<string>> codesToActorIds,
        IReadOnlyDictionary<string, string> codesToDirectorIds,
        IReadOnlyDictionary<string, string> imdbToMovieLensIds,
        IReadOnlyDictionary<string,(string, List<string>)> actorIdsToNameAndStarredFilms)
    {
        var actorIds =  codesToActorIds.ContainsKey(code) ? codesToActorIds[code] : new List<string>();
        var actors = new HashSet<string>();
        foreach (var actorId in actorIds.Where(actorIdsToNameAndStarredFilms.ContainsKey))
        {
            actors.Add(actorIdsToNameAndStarredFilms[actorId].Item1);
        }

        
        var tagIds = imdbToMovieLensIds.ContainsKey(code) && movieLensCodesToTagIds.ContainsKey(imdbToMovieLensIds[code]) ?
            movieLensCodesToTagIds[imdbToMovieLensIds[code]] : new List<string>();
        var tags = new HashSet<string>();
        foreach (var tagCode in tagIds)
        {
            tags.Add(tagCodesToTitles[tagCode]);
        }
        
        return new DefaultMovie
        {
            Title = codesToTitles[code],
            Rate = codesToRatings.ContainsKey(code) ? codesToRatings[code] : null,
            Director = codesToDirectorIds.ContainsKey(code) ?  actorIdsToNameAndStarredFilms[codesToDirectorIds[code]].Item1 : null,
            Actors = actors.ToList(),
            Tags = tags.ToHashSet()
        };
    }
}