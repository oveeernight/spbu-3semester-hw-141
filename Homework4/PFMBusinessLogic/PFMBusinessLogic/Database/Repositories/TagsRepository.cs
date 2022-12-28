using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PFMBusinessLogic.Database.StorageElements;
using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Database.Repositories;

public class TagsRepository : ITagsRepository
{
    private DatabaseContext dbContext;
    public TagsRepository(DatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public async Task<Tag> ReadAsync(string name)
    {
        var tag = await  dbContext.TagsStorage.FindAsync(name);
        return ToModel(tag);
    }

    public async Task<Tag[]> ReadManyAsync(string[] names)
    {
        var tags = new List<Tag>();
        foreach (var name in names)
        {
            tags.Add(await ReadAsync(name));
        }

        return tags.ToArray();
    }

    private static Tag ToModel(TagStorageElement storageElement)
    {
        var stringMovies = JsonSerializer.Deserialize<List<Movie>>(storageElement.Movies);
        return new Tag()
        {
            Name = storageElement.Name,
            Movies = stringMovies.Select(movie => new Movie() { Title = movie.Title }).ToArray()
        };
    }
}