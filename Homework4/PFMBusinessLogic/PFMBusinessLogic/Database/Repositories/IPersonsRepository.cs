using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Database.Repositories;

public interface IPersonsRepository
{
    public Task<Person> ReadAsync(string name);
    public Task<Person[]> ReadManyAsync(string[] names);
}