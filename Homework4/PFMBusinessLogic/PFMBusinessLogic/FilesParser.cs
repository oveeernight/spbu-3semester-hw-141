using System.Globalization;

namespace PFMBusinnecLogic;

public class FilesParser
{
    public static Dictionary<string, string> GetTitleByMovieCode()
    {
        var lines = File.ReadAllLines("MovieCodes_IMDB.tsv");
        var result = new Dictionary<string, string>();
        throw new NotImplementedException();
    }

    // key - movie id, value - list of roles
    public static (Dictionary<string, List<string>>, Dictionary<string, string>) GetActorsAndDirectorsByMovieId()
    {
        var lines = File.ReadAllLines("ActorsDirectorsCodes_IMDB.tsv");
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
    public static Dictionary<string, (string, List<string>)> GetStarredFilmsAndTitleByActorId()
    {
        var lines = File.ReadAllLines("ActorsDirectorsNames_IMDB.txt");
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

    public static Dictionary<string, string> GetRatingByMovieId()
    {
        var lines = File.ReadAllLines("Ratings_IMDB.tsv");
        return lines.Skip(1).AsParallel().ToDictionary(keySelector: line => line[..line.IndexOf('\t')], elementSelector:
            line =>
            {
                var i = line.IndexOf('.');
                return line[(i - 1)..(i + 2)];
            });
    }

    // key - movieId, value - list of relevant tags
    public static Dictionary<string, List<string>> GetRelevantTagsByMovieLensId()
    {
        var result = new Dictionary<string, List<string>>();
        var lines = File.ReadAllLines("TagScores_MovieLens.csv").Skip(1);
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

    public static Dictionary<string, string> GetMovieLensIdByImdbId()
    {
        var lines = File.ReadAllLines("links_IMDB_MovieLens.csv");
        return lines.Skip(1).AsParallel().ToDictionary(keySelector: line =>
        {
            var span = line.AsSpan();
            var i = span.IndexOf(',');
            span = span[(i + 1)..];
            i = span.IndexOf(',');
            return span[..i].ToString();
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

    public static Dictionary<string, string> GetTagById()
    {
        var lines = File.ReadAllLines("TagCodes_MovieLens.csv");
        return lines.Skip(1).AsParallel().ToDictionary(keySelector: line => line[..line.IndexOf(',')],
            elementSelector: line => line[(line.IndexOf(',') + 1)..]);
    }
}