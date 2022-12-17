using PFMBusinessLogic.DefaultModels;
using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Extensions;

public static class DictionariesExtensions
{
    public static Dictionary<string, Movie> ToMovieModels(this Dictionary<string, DefaultMovie> defaultMovies)
    {
        var movies = new Dictionary<string, Movie>();
        foreach (var (title, defaultMovie) in defaultMovies)
        {
            movies.Add(title,new Movie
            {
                Director = defaultMovie.Director,
                Rate = defaultMovie.Rate,
                Title = title,
                Persons = defaultMovies.Count == 0 ? new HashSet<Person>() : defaultMovie.Actors.Select(person => new Person{Name = person, Movies = new HashSet<Movie>()}).ToArray(),
                Tags = defaultMovie.Tags.Count == 0 ? new HashSet<Tag>() : defaultMovie.Tags.Select(tag => new Tag{Name = tag, Movies = new HashSet<Movie>()}).ToArray()
            });
        }

        return movies;
    }

    public static Dictionary<string, Movie> WithTags(this Dictionary<string, Movie> movies,
        Dictionary<string, Tag> tags)
    {
        foreach (var (_, movie) in movies)
        {
            var newTags = new HashSet<Tag>();
            foreach (var tag in movie.Tags)
            {
                newTags.Add(tags[tag.Name]);
            }

            movie.Tags = newTags;
        }

        return movies;
    }
    
    public static Dictionary<string, Movie> WithPersons(this Dictionary<string, Movie> movies,
        Dictionary<string, Person> persons)
    {
        foreach (var (_, movie) in movies)
        {
            var newPersons = new HashSet<Person>();
            foreach (var person in movie.Persons)
            {
                newPersons.Add(persons[person.Name]);
            }

            movie.Persons = newPersons;
        }

        return movies;
    }

    public static Dictionary<string, Person> ToPersonModels(this Dictionary<string, HashSet<string>> defaultPersons,
        Dictionary<string, Movie> movies)
    {
        var persons = new Dictionary<string, Person>();
        foreach (var (name, defaultMovies) in defaultPersons)
        {
            persons.Add(name, new Person
            {
                Name = name,
                Movies = defaultMovies.Count == 0 ? new HashSet<Movie>() : defaultMovies.Select(movie => movies[movie]).ToHashSet()
            });
        }

        return persons;
    }

    public static Dictionary<string, Tag> ToTagModels(this Dictionary<string, HashSet<string>> defaultTags,
        Dictionary<string, Movie> movies)
    {
        var tags = new Dictionary<string, Tag>();
        foreach (var (name, defaultMovies) in defaultTags)
        {
            tags.Add(name, new Tag
            {
                Name = name,
                Movies = defaultMovies.Count == 0 ? new HashSet<Movie>() : defaultMovies.Select(movie => movies[movie]).ToHashSet()
            }); 
        }

        return tags;
    }
}