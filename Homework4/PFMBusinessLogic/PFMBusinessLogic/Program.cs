using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using PFMBusinnecLogic;

class Program
{
    public static void Main(string[] args)
    {
        var timer = new Stopwatch();
        timer.Start();
        var (actors, directors) = FilesParser.GetActorsAndDirectorsByMovieId();
        var codesToTitles = FilesParser.GetTitleByMovieCode();
        var tagCodesToTitles = FilesParser.GetTagById();
        var codesToRatings = FilesParser.GetRatingByMovieId();
        var movieLensCodeToTagIds = FilesParser.GetRelevantTagsByMovieLensId();
        var imdbToMovieLensIds = FilesParser.GetMovieLensIdByImdbId();
        var actorIdsToNameAndStarredFilms = FilesParser.GetStarredFilmsAndTitleByActorId();

        var movies = GetMovies(codesToTitles: codesToTitles,
            codesToActors: actors,
            codesToDirectors: directors,
            codesToRatings: codesToRatings,
            tagCodesToTitles: tagCodesToTitles,
            imdbToMovieLensIds: imdbToMovieLensIds,
            movieLensCodesToTagIds: movieLensCodeToTagIds);

        var actorsDct = GetActorsAndDirectors(movies: movies,
            actorIdsToNameAndStarredFilms: actorIdsToNameAndStarredFilms,
            codesToTitles: codesToTitles
        );

        var tags = GetTags(movies);
        timer.Stop();
        var el = timer.Elapsed;
        Console.WriteLine($"{el.Seconds}:{el.Milliseconds}");
    }

    public static Dictionary<string, Movie> GetMovies(Dictionary<string, string> codesToTitles,
        Dictionary<string, string> codesToRatings,
        Dictionary<string, List<string>> movieLensCodesToTagIds,
        Dictionary<string, string> tagCodesToTitles,
        Dictionary<string, List<string>> codesToActors,
        Dictionary<string, string> codesToDirectors,
        Dictionary<string, string> imdbToMovieLensIds)
    {
        var result = new Dictionary<string, Movie>();
        foreach (var (key, value) in codesToTitles)
        {
            var movie = GetMovieById(key, codesToTitles: codesToTitles,
                codesToActors: codesToActors,
                codesToDirectors: codesToDirectors,
                codesToRatings: codesToRatings,
                tagCodesToTitles: tagCodesToTitles,
                imdbToMovieLensIds: imdbToMovieLensIds,
                movieLensCodesToTagIds: movieLensCodesToTagIds);
            if (!result.ContainsKey(value))
                result.Add(value, movie);
        }

        return result;
    }

    private static Dictionary<string, HashSet<Movie>> GetActorsAndDirectors(
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






private static Movie GetMovieById(string key, Dictionary<string, string> codesToTitles,
        Dictionary<string, string> codesToRatings,
        Dictionary<string, List<string>> movieLensCodesToTagIds,
        Dictionary<string, string> tagCodesToTitles,
        Dictionary<string, List<string>> codesToActors,
        Dictionary<string, string> codesToDirectors,
        Dictionary<string, string> imdbToMovieLensIds)
    {
        if (!codesToTitles.ContainsKey(key)) return new Movie();
        return new Movie
        {
            Title = codesToTitles[key],
            Rate = codesToRatings.ContainsKey(key) ? codesToRatings[key] : null,
            Director = codesToDirectors.ContainsKey(key) ? codesToDirectors[key] : null,
            Actors = codesToActors.ContainsKey(key) ? codesToActors[key].ToHashSet() : new HashSet<string>(),
            Tags = imdbToMovieLensIds.ContainsKey(key)
                ? movieLensCodesToTagIds[imdbToMovieLensIds[key]].Select(id => tagCodesToTitles[id]).ToHashSet()
                : new HashSet<string>()
        };
    }
}

