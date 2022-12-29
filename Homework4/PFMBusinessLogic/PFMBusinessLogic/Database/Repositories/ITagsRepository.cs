using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Database.Repositories;

public interface ITagsRepository
{
    public Task<Tag> ReadAsync(string name);
    public Task<Tag[]> ReadManyAsync(string[] names);
}