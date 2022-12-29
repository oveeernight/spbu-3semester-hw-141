using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using PFMBusinessLogic.Database.StorageElements;
using PFMBusinessLogic.Models;

namespace PFMBusinessLogic.Database;

public class DatabaseContext : DbContext
{
    // это надо раскомментить если хочется заполнить бд из консольного приложения
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     var connectionString =
    //         "Server=localhost;Port=5432;Database=moviesDB;Username=postgres;Password=postgres";
    //     optionsBuilder.UseNpgsql(connectionString);
    // }

     public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options){}

     public DbSet<MovieStorageElement> MoviesStorage { get; set; }
    public DbSet<PersonStorageElement> PersonsStorage { get; set; }
    public DbSet<TagStorageElement> TagsStorage { get; set; }
    
}