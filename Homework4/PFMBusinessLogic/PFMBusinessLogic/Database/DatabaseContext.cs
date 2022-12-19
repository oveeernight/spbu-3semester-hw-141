using Microsoft.EntityFrameworkCore;
using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Database;

public class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString =
            "Server=localhost;Port=5432;Database=movies;Username=postgres;Password=postgres";
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>().HasOne(movie => movie.Director);
        modelBuilder.Entity<Movie>().HasMany(movie => movie.Actors).WithMany(actor => actor.Movies);
        modelBuilder.Entity<Movie>().HasMany(movie => movie.Tags).WithMany(tag => tag.Movies);

        // modelBuilder.Entity<Tag>().HasMany(tag => tag.Movies).WithMany(movie => movie.Tags);
        //
        // modelBuilder.Entity<Person>().HasMany(person => person.Movies).WithMany(movie => movie.Actors);
    }

    public DbSet<Movie> MoviesStorage { get; set; }
    public DbSet<Person> PersonsStorage { get; set; }
    public DbSet<Tag> TagsStorage { get; set; }
}