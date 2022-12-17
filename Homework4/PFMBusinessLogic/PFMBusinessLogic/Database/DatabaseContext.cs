using Microsoft.EntityFrameworkCore;
using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Database;

public class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString =
            "Host=localhost;Port=8091;Database=postgres;Username=postgres;Password=postgres";
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("movies");
        modelBuilder.Entity<Movie>().HasMany(movie => movie.Actors).WithMany(actor => actor.Movies);
        modelBuilder.Entity<Movie>().HasMany(movie => movie.Tags).WithMany(tag => tag.Movies);
    }

    public DbSet<Movie> MoviesStorage { get; set; }
    public DbSet<Person> ActorsStorage { get; set; }
    public DbSet<Tag> TagsStorage { get; set; }
}