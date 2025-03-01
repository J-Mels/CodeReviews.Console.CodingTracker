using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    public static class MenuUtilities
    {
        public static void CreateSessionMenu()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[yellow]Create a Coding Session[/]");

            // Get start time (required)
            var (startInput, startTime) = GetDateTimeInput("start time", required: true);
            if (startInput.Trim().ToLower() == "q")
            {
                AnsiConsole.MarkupLine("[yellow]Operation cancelled.[/]");
                AnsiConsole.MarkupLine("Press any key to return...");
                Console.ReadKey();
                return;
            }

            // Get end time (optional)
            var (endInput, endTime) = GetDateTimeInput("end time", required: false);
            if (endInput.Trim().ToLower() == "q")
            {
                AnsiConsole.MarkupLine("[yellow]Operation cancelled.[/]");
                AnsiConsole.MarkupLine("Press any key to return...");
                Console.ReadKey();
                return;
            }

            CodingController.CreateSession(startTime!.Value, endTime); // ! safe due to required=true
            AnsiConsole.MarkupLine("[green]Session created successfully![/]");
            AnsiConsole.MarkupLine("Press any key to return...");
            Console.ReadKey();
        }

        public static void UpdateSessionMenu()
        {
            AnsiConsole.Clear();

            //  ask user to select an ID from the list of sessions
            // --Use AnsiConsole.Prompt(new SelectionPrompt<string>().Title().PageSize().MoreChoicesText().AddChoices())
            // --documentation -- https://spectreconsole.net/api/spectre.console/selectionprompt_1/
        }

        public static void DeleteSessionMenu()
        {
            AnsiConsole.Clear();

            //  ask user to select an ID from the list of sessions
            // --Use AnsiConsole.Prompt(new SelectionPrompt<string>().Title().PageSize().MoreChoicesText().AddChoices())
            // --documentation - https://spectreconsole.net/api/spectre.console/selectionprompt_1/

        }
    }
}
