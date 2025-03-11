using CodingTracker;
using Spectre.Console;
using System;

class Program
{
    static void Main()
    {
        bool programRunning = true;

        try
        {
            DatabaseInitializer.InitializeDatabase();
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Failed to initialize database: {ex.Message}");
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadKey();
            return; // Exit program since DB is essential
        }

        while (programRunning)
        {
            try
            {
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
                        AnsiConsole.Clear();
                        MenuUtilities.DeleteSessionMenu();
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
