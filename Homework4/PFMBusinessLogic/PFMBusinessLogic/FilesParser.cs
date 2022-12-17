using System.Globalization;

namespace PFMBusinessLogic;

public class FilesParser
{
    public static Dictionary<string, string> GetTitleByMovieCode(string path)
    {
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
    public static (Dictionary<string, List<string>>, Dictionary<string, string>) GetActorsAndDirectorsByMovieId(string path)
    {
        var lines = File.ReadAllLines(path);
        var actors = new Dictionary<string, List<string>>();
        var directors = new Dictionary<string, string>();
        Parallel.ForEach(lines, line =>
        {
            var span = line.AsSpan();
            var i = span.IndexOf('\t');
            var movieId = span[..i];
            span = span[(i + 1)..];

            i = span.IndexOf('\t');
            span = span[(i + 1)..];

            i = span.IndexOf('\t');
            var actorId = span[..i];

            i = span.IndexOf(('\t'));
            span = span[(i + 1)..];

            i = span.IndexOf(('\t'));
            var role = span[..i].ToString();
            if (role is "actor" or "actress")
            {
                lock (actors)
                {
                    if (actors.ContainsKey(movieId.ToString()))
                    {
                        actors[movieId.ToString()].Add(actorId.ToString());
                    }
                    else
                    {
                        actors.Add(movieId.ToString(), new List<string> { actorId.ToString() });
                    }
                }
            }
            else if (role is "director")
            {
                lock (directors)
                {
                    if (!directors.ContainsKey(movieId.ToString()))
                    {
                        directors.Add(movieId.ToString(),  actorId.ToString());
                    }
                }
                
            }
        });
        
        return (actors, directors);
    }

    // key - actorId, value - list of filmIds, name
    public static Dictionary<string, (string, List<string>)> GetStarredFilmsAndTitleByPersonId(string path)
    {
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
        var lines = File.ReadAllLines(path);
        return lines.Skip(1).AsParallel().ToDictionary(keySelector: line => line[..line.IndexOf(',')],
            elementSelector: line => line[(line.IndexOf(',') + 1)..]);
    }
}