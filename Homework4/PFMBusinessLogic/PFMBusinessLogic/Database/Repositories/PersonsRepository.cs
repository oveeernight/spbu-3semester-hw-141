using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PFMBusinessLogic.Database.StorageElements;
using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Database.Repositories;

public class PersonsRepository : IPersonsRepository
{
    private DatabaseContext dbContext;

    public PersonsRepository(DatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Person> ReadAsync(string name)
    {
        var person = await dbContext.PersonsStorage.FindAsync(name);
        return ToModel(person);
    }

    public async Task<Person[]> ReadManyAsync(string[] names)
    {
        var persons = new List<Person>();
        foreach (var name in names)
        {
            persons.Add(await ReadAsync(name));
        }

        return persons.ToArray();
    }
    
    private static Person ToModel(PersonStorageElement storageElement)
    {
        var stringMovies = JsonSerializer.Deserialize<string[]>(storageElement.Movies);
        return new Person()
        {
            Name = storageElement.Name,
            Movies = stringMovies.Select(title => new Movie() { Title = title }).ToArray()
        };
    }
}