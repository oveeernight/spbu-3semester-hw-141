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
        var stringActors = JsonSerializer.Deserialize<string[]>(storageElement.Actors);
        var stringDirectors = JsonSerializer.Deserialize<string[]>(storageElement.Directors);
        var stringRelated = JsonSerializer.Deserialize<string[]>(storageElement.Top10Related);
        var stringTags = JsonSerializer.Deserialize<string[]>(storageElement.Tags);

        return new Movie
        {
            Title = storageElement.Title,
            Rate = storageElement.Rate,
            Actors = stringActors.Select(a => new Person() { Name = a }).ToArray(),
            Directors = stringDirectors.Select(d => new Person() { Name = d }).ToArray(),
            Tags = stringTags.Select(t => new Tag() { Name = t }).ToArray(),
            Top10Related = stringRelated.Select(m => new Movie() { Title = m }).ToArray()
        };
    }
}