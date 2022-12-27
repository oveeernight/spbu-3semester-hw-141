using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Services;

public class MoviesService : IMoviesService
{
    public async Task<Movie> GetMovie(string title)
    {
        throw new NotImplementedException();
    }

    public async Task<Person> GetPerson(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<Tag> GetTag(string name)
    {
        throw new NotImplementedException();
    }
}