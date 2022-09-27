using System.Text.Json;

namespace FileManager;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        using (var fs = new FileStream("authorization.json", FileMode.OpenOrCreate))
        {
            var auth = new AuthCredits() { Login = "asd", Password = "Vetochka" };
            JsonSerializer.Serialize(fs, auth);
        }
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }    
}