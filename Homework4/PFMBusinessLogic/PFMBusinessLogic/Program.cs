using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization.Json;
using PFMBusinnecLogic;

class Program
{
    public static void Main(string[] args)
    {
        var timer = new Stopwatch();
        timer.Start();
         var (codesToActorIds, codesToDirectorIds) = FilesParser.GetActorsAndDirectorsByMovieId();
        var codesToTitles = FilesParser.GetTitleByMovieCode();
         var tagCodesToTitles = FilesParser.GetTagById();
         var codesToRatings = FilesParser.GetRatingByMovieId();
         var movieLensCodeToTagIds = FilesParser.GetRelevantTagsByMovieLensId();
         var imdbToMovieLensIds = FilesParser.GetMovieLensIdByImdbId();
         var actorIdsToNameAndStarredFilms = FilesParser.GetStarredFilmsAndTitleByActorId();
        
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
        
         Console.WriteLine(movies.Count);
         Console.WriteLine(tags.Count);
         Console.WriteLine(actorsDct.Count);
         var el = timer.Elapsed;
         Console.WriteLine($"{el.Seconds}:{el.Milliseconds}");
         
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

