using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Database.Repositories;

public interface IMoviesRepository
{
    public Task<Movie> ReadAsync(string title);
    public Task<Movie[]> ReadManyAsync(string[] titles);
}