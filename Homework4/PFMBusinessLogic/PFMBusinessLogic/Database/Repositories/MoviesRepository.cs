using System.Text.Json;
using PFMBusinessLogic.Database.StorageElements;
using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Database.Repositories;

public class MoviesRepository : IMoviesRepository
{
    private DatabaseContext dbContext;

    public MoviesRepository(DatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    
    public async Task<Movie> ReadAsync(string title)
    {
        var movie = await dbContext.MoviesStorage.FindAsync(title);
        if (movie == null) return new Movie();
        return ToModel(movie);
    }

    public async Task<Movie[]> ReadManyAsync(string[] titles)
    {
        var movies = new List<Movie>();

        foreach (var title in titles)
        {
            movies.Add(await ReadAsync(title));
        }

        return movies.ToArray();
    }

    private static Movie ToModel(MovieStorageElement storageElement)
    {
        var actors = JsonSerializer.Deserialize<Person[]>(storageElement.Actors);
        var directors = JsonSerializer.Deserialize<Person[]>(storageElement.Directors);
        var related = JsonSerializer.Deserialize<Movie[]>(storageElement.Top10Related);
        var tags = JsonSerializer.Deserialize<Tag[]>(storageElement.Tags);

        return new Movie
        {
            Title = storageElement.Title,
            Rate = storageElement.Rate,
            Actors = actors ?? Array.Empty<Person>(),
            Directors = directors ?? Array.Empty<Person>(),
            Tags = tags ?? Array.Empty<Tag>(),
            Top10Related = related ?? Array.Empty<Movie>()
        };
    }
}