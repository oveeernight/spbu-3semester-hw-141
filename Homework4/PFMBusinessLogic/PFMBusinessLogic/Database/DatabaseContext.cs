using Microsoft.EntityFrameworkCore;
using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Database;

public class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString =
            "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("movies");
        modelBuilder.Entity<Movie>().HasMany(movie => movie.Actors).WithMany()
        
    }

    public DbSet<Movie> MoviesStorage { get; set; }
    public DbSet<Actor> ActorsStorage { get; set; }
    public DbSet<Tag> TagsStorage { get; set; }
}