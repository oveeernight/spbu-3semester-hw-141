// using System.Diagnostics;
// using PFMBusinessLogic.Database;
// using PFMBusinessLogic.Extensions;
// using PFMBusinessLogic.Providers;
//
//
// namespace PFMBusinessLogic;
//
// public class Program
// {
//     public static void Main(string[] args)
//     {
//         var pathTemplate = "../../../Files";
//         var timer = new Stopwatch();
//         timer.Start();
//         var (codesToActorIds, codesToDirectorIds) =
//             FilesParser.GetPersonsByMovieId($"{pathTemplate}/ActorsDirectorsCodes_IMDB.tsv");
//         var codesToTitles = FilesParser.GetTitleByMovieCode($"{pathTemplate}/MovieCodes_IMDB.tsv");
//         var tagCodesToTitles = FilesParser.GetTagById($"{pathTemplate}/TagCodes_MovieLens.csv");
//         var codesToRatings = FilesParser.GetRatingByMovieId($"{pathTemplate}/Ratings_IMDB.tsv");
//         var movieLensCodeToTagIds = FilesParser.GetRelevantTagsByMovieLensId($"{pathTemplate}/TagScores_MovieLens.csv");
//         var imdbToMovieLensIds = FilesParser.GetMovieLensIdByImdbId($"{pathTemplate}/links_IMDB_MovieLens.csv");
//         var personIdsToNameAndStarredFilms =
//             FilesParser.GetStarredFilmsAndTitleByPersonId($"{pathTemplate}/ActorsDirectorsNames_IMDB.txt");
//
//         var movies = DictionariesProvider.GetMovies(codesToTitles: codesToTitles,
//             codesToActorIds: codesToActorIds,
//             codesToDirectorIds: codesToDirectorIds,
//             codesToRatings: codesToRatings,
//             tagCodesToTitles: tagCodesToTitles,
//             imdbToMovieLensIds: imdbToMovieLensIds,
//             movieLensCodesToTagIds: movieLensCodeToTagIds,
//             personIdsToNameAndStarredFilms: personIdsToNameAndStarredFilms
//         );
//         Console.WriteLine($"\tMovies count: {movies.Count}");
//
//         var persons = DictionariesProvider.GetPersons(movies: movies);
//         Console.WriteLine($"\tPersons count: {persons.Count}");
//
//         var tags = DictionariesProvider.GetTags(movies);
//         Console.WriteLine($"\tTags count: {tags.Count}");
//         
//         var el = timer.Elapsed;
//         Console.WriteLine($"{el.Minutes:00}:{el.Seconds:00}:{el.Milliseconds:00}");
//         
//         Console.WriteLine("Filling related movies");
//         movies = movies.WithRelatedMovies(persons, tags);
//          el = timer.Elapsed;
//         Console.WriteLine($"{el.Minutes:00}:{el.Seconds:00}:{el.Milliseconds:00}");
//
//         Console.WriteLine("Writing models to database");
//         Console.WriteLine();
//
//         using var db = new DatabaseContext();
//         db.Database.EnsureDeleted();
//         db.Database.EnsureCreated();
//         
//         Console.WriteLine("Writing tags");
//         db.TagsStorage.AddRange(tags.Values.Select(tag => tag.ToStorageElement()));
//         
//         Console.WriteLine("Writing persons");
//         db.PersonsStorage.AddRange(persons.Values.Select(person => person.ToStorageElement()));
//         
//         Console.WriteLine("Writing movies");
//         db.MoviesStorage.AddRange(movies.Select(movie => movie.ToStorageElement()));
//
//         Console.WriteLine("Saving changes");
//         db.SaveChanges();
//         
//         el = timer.Elapsed;
//         Console.WriteLine($"{el.Minutes:00}:{el.Seconds:00}:{el.Milliseconds:00}");
//     }
// }