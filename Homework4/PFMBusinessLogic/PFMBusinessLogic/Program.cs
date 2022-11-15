using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization.Json;
using PFMBusinnecLogic;

class Program
{
    public static void Main(string[] args)
    {
        var pathTemplate = "../../../Files";
        var timer = new Stopwatch();
        timer.Start();
         var (codesToActorIds, codesToDirectorIds) = 
             FilesParser.GetActorsAndDirectorsByMovieId($"{pathTemplate}/ActorsDirectorsCodes_IMDB.tsv");
        var codesToTitles = FilesParser.GetTitleByMovieCode($"{pathTemplate}/MovieCodes_IMDB.tsv");
         var tagCodesToTitles = FilesParser.GetTagById($"{pathTemplate}/TagCodes_MovieLens.csv");
         var codesToRatings = FilesParser.GetRatingByMovieId($"{pathTemplate}/Ratings_IMDB.tsv");
         var movieLensCodeToTagIds = FilesParser.GetRelevantTagsByMovieLensId($"{pathTemplate}/TagScores_MovieLens.csv");
         var imdbToMovieLensIds = FilesParser.GetMovieLensIdByImdbId($"{pathTemplate}/links_IMDB_MovieLens.csv");
         var actorIdsToNameAndStarredFilms = FilesParser.GetStarredFilmsAndTitleByActorId($"{pathTemplate}/ActorsDirectorsNames_IMDB.txt");
        
         var movies = DictionariesProvider.GetMovies(codesToTitles: codesToTitles,
             codesToActorIds: codesToActorIds,
             codesToDirectorIds: codesToDirectorIds,
             codesToRatings: codesToRatings,
             tagCodesToTitles: tagCodesToTitles,
             imdbToMovieLensIds: imdbToMovieLensIds,
             movieLensCodesToTagIds: movieLensCodeToTagIds,
             actorIdsToNameAndStarredFilms: actorIdsToNameAndStarredFilms
             );
        
         var actorsDct = DictionariesProvider.GetActorsAndDirectors(movies: movies,
             actorIdsToNameAndStarredFilms: actorIdsToNameAndStarredFilms,
             codesToTitles: codesToTitles
         );
        
         var tags = DictionariesProvider.GetTags(movies);
         timer.Stop();
         
         var el = timer.Elapsed;
         Console.WriteLine($"{el.Minutes}:{el.Seconds}:{el.Milliseconds}");
         
         InputLoop(movies, actorsDct, tags);
    }

    private static void InputLoop(Dictionary<string, Movie> movies, Dictionary<string, HashSet<Movie>> actors,
        Dictionary<string, HashSet<Movie>> tags)
    {
        Console.WriteLine("-m [title] - print information about movie with the specified title \n"+
                          "-a [name] - print information about actor or director with the specified name \n" +
                          "-t [tag title] - print information about movies related to the specified tag \n" +
                          "exit to kill program");
        var input = Console.ReadLine();
        while (input is not "exit")
        {
            var key = input[..input.IndexOf(' ')];
            var value = input[(input.IndexOf(' ') + 1)..];
            switch (key)
            {
                case "-m":
                    Console.WriteLine(movies.ContainsKey(value) ? GetMovieInfo(movies[value]) : "unknown fulm");
                    break;
                case "-a":
                    Console.WriteLine(actors.ContainsKey(value) ? GetMovieTitles(actors[value]) : "unknown actor");
                    break;
                case "-t":
                    Console.WriteLine(tags.ContainsKey(value) ? GetMovieTitles(tags[value]) : "unknown tag");
                    break;
                
            }
            input = Console.ReadLine();
        }
    }

    private static string GetMovieInfo(Movie movie)
    {
        return $"Title - {movie.Title} \n" +
               $"Rate - {movie.Rate}\n" +
               $"Director - {movie.Director}\n " +
               $"Actors - {string.Join(',', movie.Actors)}\n" +
               $"Tags - {string.Join(',', movie.Tags)}";
    }

    private static string GetMovieTitles(IEnumerable<Movie> movies)
    {
        return string.Join(',', movies.Select(movie => movie.Title));
    }
}

