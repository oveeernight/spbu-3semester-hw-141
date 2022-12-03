using System.Diagnostics;
using PFMBusinessLogic.Extensions;
using PFMBusinessLogic.Models;
using PFMBusinessLogic.Providers;

namespace PFMBusinessLogic;

public class Program 
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
        var actorIdsToNameAndStarredFilms =
            FilesParser.GetStarredFilmsAndTitleByActorId($"{pathTemplate}/ActorsDirectorsNames_IMDB.txt");

        var defaultMovies = DefaultDictionariesProvider.GetMovies(codesToTitles: codesToTitles,
            codesToActorIds: codesToActorIds,
            codesToDirectorIds: codesToDirectorIds,
            codesToRatings: codesToRatings,
            tagCodesToTitles: tagCodesToTitles,
            imdbToMovieLensIds: imdbToMovieLensIds,
            movieLensCodesToTagIds: movieLensCodeToTagIds,
            actorIdsToNameAndStarredFilms: actorIdsToNameAndStarredFilms
        );

        var persons = DefaultDictionariesProvider.GetActorsAndDirectors(movies: defaultMovies,
            actorIdsToNameAndStarredFilms: actorIdsToNameAndStarredFilms,
            codesToTitles: codesToTitles
        );

        var tags = DefaultDictionariesProvider.GetTags(defaultMovies);
        timer.Stop();

        var el = timer.Elapsed;
        Console.WriteLine($"{el.Minutes}:{el.Seconds}:{el.Milliseconds}");

        Console.WriteLine("Starting to write models in db");

        var movieModelsWithoutTagsAndPersons = defaultMovies.ToMovieModels();
        var personModels = persons.ToPersonModels(movieModelsWithoutTagsAndPersons);
        var tagModels = tags.ToTagModels(movieModelsWithoutTagsAndPersons);
        var movies = movieModelsWithoutTagsAndPersons.WithPersons(personModels)
            .WithTags(tagModels);
    }

}