using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PFMBusinessLogic.Models;
using PFMBusinnecLogic.Models;

namespace PFMBusinnecLogic.Database;

public class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString =
            "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";
        optionsBuilder.UseNpgsql(connectionString);
    }

    public DbSet<Movie> MoviesStorage { get; set; }
    public DbSet<Actor> ActorsStorage { get; set; }
    public DbSet<Tag> TagsStorage { get; set; }
}