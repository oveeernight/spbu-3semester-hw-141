class Program
{
    static Random random = new();
    static CancellationTokenSource cts = new ();


   public static async Task Main(string[] args)
    {
        var start = Task.Run(() => ShowSplash());

        var chain1 = start.ContinueWith(ant => RequestLicense())
            .ContinueWith(ant => SetupMenus(), TaskContinuationOptions.OnlyOnRanToCompletion);

        var chain2 = start.ContinueWith(ant => CheckForUpdate())
            .ContinueWith(ant => DownloadUpdate(), TaskContinuationOptions.OnlyOnRanToCompletion);

        var end = Task.WhenAll(new[] {chain1, chain2})
            .ContinueWith(ant => HideSplash()).ContinueWith(ant => DisplayWelcomeScreen());
        await end;
        cts.Token.Register(() => Console.WriteLine("loading failed"));
    }

    static void ShowSplash()
    {
        Console.WriteLine("ShowSplash ");
    }

    static void RequestLicense()
    {
        if (random.NextDouble() < 0.5)
        {
            cts.Cancel();
        }

        Console.WriteLine("RequestLicense");
    }

    static void SetupMenus()
    {
        Console.WriteLine("SetupMenus ");
    }

    static void CheckForUpdate()
    {

        if (random.NextDouble() < 0.5)
        {
            cts.Cancel();
        }

        Console.WriteLine("CheckForUpdate");
    }

    static void DownloadUpdate()
    {
        Console.WriteLine("DownloadUpdate");
    }

    static void DisplayWelcomeScreen()
    {
        Console.WriteLine("DisplayWelcomeScreen");
    }

     static void HideSplash()
    {
        Console.WriteLine("HideSplash");
    }
}