using Microsoft.EntityFrameworkCore;
using PFMBusinessLogic.Database.StorageElements;
using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Database;

public class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString =
            "Server=localhost;Port=5432;Database=moviesDB;Username=postgres;Password=postgres";
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<Movie>().HasMany(movie => movie.Top10Related);
        // modelBuilder.Entity<Movie>().HasMany(movie => movie.Tags).WithMany(tag => tag.Movies);
        // modelBuilder.Entity<Movie>().HasMany(movie => movie.Actors).WithMany(person => person.Movies);
        // modelBuilder.Entity<Movie>().HasMany(movie => movie.Directors).WithMany(person => person.Movies);
    }

    public DbSet<MovieStorageElement> MoviesStorage { get; set; }
    public DbSet<PersonStorageElement> PersonsStorage { get; set; }
    public DbSet<TagStorageElement> TagsStorage { get; set; }
}