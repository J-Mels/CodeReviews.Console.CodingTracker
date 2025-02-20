using CodingTracker;
using System;

class Program
{
    static void Main()
    {
        //try
        //{
        //    ConfigManager configManager = new ConfigManager();
        //    Console.WriteLine($"Database Path: {configManager.DatabasePath}");
        //    Console.WriteLine($"Connection String: {configManager.ConnectionString}");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Failed to load config: {ex.Message}");
        //}

        DatabaseInitializer.InitializeDatabase();

        try
        {
            DateTime start = DateTime.Today;
            DateTime end = DateTime.Now;
            CodingSession codingSession = new CodingSession(start, end);

            Console.WriteLine(codingSession.StartTime);
            Console.WriteLine(codingSession.EndTime);
            Console.WriteLine(codingSession.Duration);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Console.ReadKey();
    }
}
