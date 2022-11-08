namespace PFMBusinnecLogic;

public static class DictionariesProvider
{
    public static Dictionary<string, Movie> GetMovies(Dictionary<string, string> codesToTitles,
        Dictionary<string, string> codesToRatings,
        Dictionary<string, List<string>> movieLensCodesToTagIds,
        Dictionary<string, string> tagCodesToTitles,
        Dictionary<string, List<string>> codesToActorIds,
        Dictionary<string, string> codesToDirectorIds,
        Dictionary<string, string> imdbToMovieLensIds,
        Dictionary<string,(string, List<string>)> actorIdsToNameAndStarredFilms)
    {
        var result = new Dictionary<string, Movie>();
        foreach (var (code, value) in codesToTitles)
        {
            var movie = GetMovieById(code, codesToTitles: codesToTitles,
                codesToActorIds: codesToActorIds,
                codesToDirectorIds: codesToDirectorIds,
                codesToRatings: codesToRatings,
                tagCodesToTitles: tagCodesToTitles,
                imdbToMovieLensIds: imdbToMovieLensIds,
                movieLensCodesToTagIds: movieLensCodesToTagIds,
                actorIdsToNameAndStarredFilms: actorIdsToNameAndStarredFilms
                );
            if (!result.ContainsKey(value))
                 result.Add(value, movie);
        }

        return result;
    }
    
    public static Dictionary<string, HashSet<Movie>> GetActorsAndDirectors(
        Dictionary<string, Movie> movies,
        Dictionary<string, (string, List<string>)> actorIdsToNameAndStarredFilms,
        Dictionary<string, string> codesToTitles)
    {
        var result = new Dictionary<string, HashSet<Movie>>();
        foreach (var (_, (name, starredFilmIds)) in actorIdsToNameAndStarredFilms)
        {
            var actorMovies = new HashSet<Movie>();
            foreach (var movie in starredFilmIds.Select(filmId =>
                         codesToTitles.ContainsKey(filmId) && movies.ContainsKey(codesToTitles[filmId])
                             ? movies[codesToTitles[filmId]]
                             : null))
            {
                if (movie != null) actorMovies.Add(movie);
            }

            if (!result.ContainsKey(name))
                result.Add(name, actorMovies);
        }

        return result;
    }
    
    public static Dictionary<string, HashSet<Movie>> GetTags(Dictionary<string, Movie> movies)
    {
        var result = new Dictionary<string, HashSet<Movie>>();
        foreach (var (title, movie) in movies)
        {
            foreach (var tag in movie.Tags)
            {
                if (result.ContainsKey(tag))
                {
                    result[tag].Add(movies[title]);
                }
                else
                {
                    result.Add(tag, new HashSet<Movie> { movies[title] });
                }
            }
        }

        return result;
    }
    
    private static Movie GetMovieById(string code,
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
        
        return new Movie
        {
            Title = codesToTitles[code],
            Rate = codesToRatings.ContainsKey(code) ? codesToRatings[code] : null,
            Director = codesToDirectorIds.ContainsKey(code) ?  actorIdsToNameAndStarredFilms[codesToDirectorIds[code]].Item1 : null,
            Actors = actors,
            Tags = tags
        };
    }
}