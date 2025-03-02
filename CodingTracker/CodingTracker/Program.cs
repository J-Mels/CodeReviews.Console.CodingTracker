using CodingTracker;
using Spectre.Console;
using System;

class Program
{
    static void Main()
    {
        bool programRunning = true;
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

        while (programRunning)
        {
            try
            {
                // TESTING
                //DateTime start = DateTime.Today;
                //DateTime end = DateTime.Now;
                //CodingSession codingSession = new CodingSession(start, end);

                //Console.WriteLine(codingSession.StartTime);
                //Console.WriteLine(codingSession.EndTime);
                //Console.WriteLine(codingSession.Duration);
                AnsiConsole.Clear();

                AnsiConsole.WriteLine("------MAIN MENU------");
                AnsiConsole.WriteLine();
                AnsiConsole.WriteLine("Select an option:");
                AnsiConsole.WriteLine();
                AnsiConsole.WriteLine("1) View all coding sessions");
                AnsiConsole.WriteLine("2) Create coding session");
                AnsiConsole.WriteLine("3) Update coding session");
                AnsiConsole.WriteLine("4) Delete coding session");
                AnsiConsole.WriteLine("0) Exit");

                string? userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        AnsiConsole.Clear();
                        TableVisualizationEngine.DisplaySessions();
                        AnsiConsole.WriteLine("Press any key to continue ...");
                        Console.ReadKey();
                        break;
                    case "2":
                        AnsiConsole.Clear();
                        MenuUtilities.CreateSessionMenu();
                        break;
                    case "3":
                        AnsiConsole.Clear();
                        MenuUtilities.UpdateSessionMenu();
                        break;
                    case "4":
                        break;
                    case "0":
                        programRunning = false;
                        break;
                    default:
                        AnsiConsole.WriteLine("Invalid entry. Try again.");
                        break;
                }
            }

            catch (Exception ex)
            {
                AnsiConsole.WriteLine($"Error: {ex.Message}");
                AnsiConsole.WriteLine("Press any key to continue ...");
                Console.ReadKey();
            }

        }
    }
}
