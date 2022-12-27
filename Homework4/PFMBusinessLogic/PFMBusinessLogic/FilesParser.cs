using System.Globalization;

namespace PFMBusinessLogic;

public class FilesParser
{
    public static Dictionary<string, string> GetTitleByMovieCode(string path)
    {
        Console.WriteLine("Parsing titles");
        var lines = File.ReadAllLines(path);
        var result = new Dictionary<string, string>();
        Parallel.ForEach(lines, line =>
        {
            var span = line.AsSpan();
            var i = span.IndexOf('\t');

            var movieId = span[..i].ToString();
            span = span[(i + 1)..];
            
            i = span.IndexOf('\t');
            span = span[(i + 1)..];
            
            i = span.IndexOf('\t');
            var title = span[..i].ToString();
            span = span[(i + 1)..];
            
            i = span.IndexOf('\t');
            var region = span[..i].ToString();

            if (region is "RU" or "GB" or "US")
            {
                lock (result)
                {
                    if (!result.ContainsKey(movieId))
                    {
                        result.Add(movieId, title);
                    }
                }
            }
        });
        return result;
    }

    // key - movie id, value - list of roles
    public static (Dictionary<string, List<string>>, Dictionary<string, List<string>>) GetPersonsByMovieId(string path)
    {
        Console.WriteLine("Parsing actors and directors starred by movie id");
        var lines = File.ReadAllLines(path);
        var codesToActors = new Dictionary<string, List<string>>();
        var codeToDirectors= new Dictionary<string, List<string>>();
        Parallel.ForEach(lines, line =>
        {
            var span = line.AsSpan();
            var i = span.IndexOf('\t');
            var movieId = span[..i].ToString();
            span = span[(i + 1)..];

            i = span.IndexOf('\t');
            span = span[(i + 1)..];

            i = span.IndexOf('\t');
            var personId = span[..i].ToString();

            i = span.IndexOf(('\t'));
            span = span[(i + 1)..];

            i = span.IndexOf(('\t'));
            var role = span[..i].ToString();
            if (role is "actor" or "actress")
            {
                lock (codesToActors)
                {
                    if (codesToActors.ContainsKey(movieId))
                    {
                        codesToActors[movieId].Add(personId);
                    }
                    else
                    {
                        codesToActors.Add(movieId, new List<string> { personId });
                    }
                }
            }
            else if (role is "director")
            {
                lock (codeToDirectors)
                {
                    if (codeToDirectors.ContainsKey(movieId))
                    {
                        codeToDirectors[movieId].Add(personId);
                    }
                    else
                    {
                        codeToDirectors.Add(movieId,  new List<string> {personId} );
                    }
                }
                
            }
        });
        
        return (codesToActors, codeToDirectors);
    }

    // key - actorId, value - list of filmIds, name
    public static Dictionary<string, (string, List<string>)> GetStarredFilmsAndTitleByPersonId(string path)
    {
        Console.WriteLine("Parsing persons starred movies");
        var lines = File.ReadAllLines(path);
        var result = new Dictionary<string, (string, List<string>)>();
        Parallel.ForEach(lines, line =>
        {
            var span = line.AsSpan();
            var i = span.IndexOf('\t');
            var actorId = span[..i];
            span = span[(i + 1)..];

            i = span.IndexOf('\t');
            var name = span[..i];
            span = span[(i + 1)..];

            i = span.IndexOf('\t');
            span = span[(i + 1)..];

            i = span.IndexOf('\t');
            span = span[(i + 1)..];

            i = span.IndexOf('\t');
            span = span[(i + 1)..];

            var movies = new List<string>();
            while (span.IndexOf(',') > 0)
            {
                var index = span.IndexOf(',');
                movies.Add(span[..index].ToString());
                span = span[(index + 1)..];
            }

            lock (result)
            {
                result.Add(actorId.ToString(), (name.ToString(), movies));
            }
        });
        return result;
    }

    public static Dictionary<string, string> GetRatingByMovieId(string path)
    {
        Console.WriteLine("Parsing rates");
        var lines = File.ReadAllLines(path);
        return lines.Skip(1).AsParallel().ToDictionary(keySelector: line => line[..line.IndexOf('\t')], elementSelector:
            line =>
            {
                var i = line.IndexOf('.');
                return line[(i - 1)..(i + 2)];
            });
    }

    // key - movieId, value - list of relevant tags
    public static Dictionary<string, List<string>> GetRelevantTagsByMovieLensId(string path)
    {
        Console.WriteLine("Parsing relevant tags");
        var result = new Dictionary<string, List<string>>();
        var lines = File.ReadAllLines(path).Skip(1);
        Parallel.ForEach(lines, line =>
        {
            var span = line.AsSpan();
            var i = span.IndexOf(',');
            var movieId = span[..i].ToString();
            span = span[(i + 1)..];

            i = span.IndexOf(',');
            var tagId = span[..i];
            span = span[(i + 1)..];

            var relevance = double.Parse(span.ToString(), CultureInfo.InvariantCulture);
            if (relevance > 0.5)
            {
                lock (result)
                {
                    if (result.ContainsKey(movieId))
                    {
                        result[movieId].Add(tagId.ToString());
                    }
                    else
                    {
                        result.Add(movieId, new List<string> { tagId.ToString() });
                    }
                }
            }
        });
        return result;
    }

    public static Dictionary<string, string> GetMovieLensIdByImdbId(string path)
    {
        Console.WriteLine("Parsing Imdb and MovieLens ids");
        var lines = File.ReadAllLines(path);
        return lines.Skip(1).AsParallel().ToDictionary(keySelector: line =>
        {
            var span = line.AsSpan();
            var i = span.IndexOf(',');
            span = span[(i + 1)..];
            i = span.IndexOf(',');
            return $"tt{span[..i]}";
        }, elementSelector: line => line[..line.IndexOf(',')]);
        
        
        // elementSelector: line =>
        // {
        //     var span = line.AsSpan();
        //     var i = span.IndexOf(',');
        //     span = span[(i + 1)..];
        //     i = span.IndexOf(',');
        //     return span[..i].ToString();
        // });
    }

    public static Dictionary<string, string> GetTagById(string path)
    {
        Console.WriteLine("Parsing tag names");
        var lines = File.ReadAllLines(path);
        return lines.Skip(1).AsParallel().ToDictionary(keySelector: line => line[..line.IndexOf(',')],
            elementSelector: line => line[(line.IndexOf(',') + 1)..]);
    }
}