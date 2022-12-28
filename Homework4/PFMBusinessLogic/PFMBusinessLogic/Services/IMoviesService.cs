using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Services;

public interface IMoviesService
{
    public Task<Movie> GetMovie(string title);
    public Task<Person> GetPerson(string name);
    public Task<Tag> GetTag(string name);
}