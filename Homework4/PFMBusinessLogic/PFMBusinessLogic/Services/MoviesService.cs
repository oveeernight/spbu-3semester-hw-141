using PFMBusinessLogic.Database.Repositories;
using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Services;

public class MoviesService : IMoviesService
{
    private IMoviesRepository moviesRepository;
    private ITagsRepository tagsRepository;
    private IPersonsRepository personsRepository;
    
    public MoviesService(IMoviesRepository moviesRepository, IPersonsRepository personsRepository, ITagsRepository tagsRepository)
    {
        this.moviesRepository = moviesRepository;
        this.tagsRepository = tagsRepository;
        this.personsRepository = personsRepository;
    }
    public async Task<Movie> GetMovie(string title)
    {
        return await moviesRepository.ReadAsync(title);
    }

    public async Task<Person> GetPerson(string name)
    {
        return await personsRepository.ReadAsync(name);
    }

    public async Task<Tag> GetTag(string name)
    {
        return await tagsRepository.ReadAsync(name);
    }
}