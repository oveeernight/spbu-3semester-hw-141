using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp1;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=moviesDB;Username=postgres;Password=postgres"));
// builder.Services.AddTransient<IMoviesRepository, MoviesRepository>();
// builder.Services.AddTransient<IPersonsRepository, PersonsRepository>();
// builder.Services.AddTransient<ITagsRepository, TagsRepository>();
// builder.Services.AddTransient<IMoviesService, MoviesService>();

await builder.Build().RunAsync();