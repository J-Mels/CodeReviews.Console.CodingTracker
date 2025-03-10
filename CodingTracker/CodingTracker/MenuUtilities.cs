using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

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
                AnsiConsole.MarkupLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            // Get end time (optional)
            var (endInput, endTime) = GetDateTimeInput("end time", required: false);
            if (endInput.Trim().ToLower() == "q")
            {
                AnsiConsole.MarkupLine("[yellow]Operation cancelled.[/]");
                AnsiConsole.MarkupLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            CodingController.CreateSession(startTime!.Value, endTime); // ! safe due to required=true
            AnsiConsole.MarkupLine("[green]Session created successfully![/]");
            AnsiConsole.MarkupLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void UpdateSessionMenu()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[yellow]Update a Coding Session[/]");

            var sessions = CodingController.GetAllSessions();
            if (!sessions.Any())
            {
                AnsiConsole.MarkupLine("[red]No sessions to update![/]");
                AnsiConsole.MarkupLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            var prompt = new SelectionPrompt<CodingSession>()
                .Title("Select a coding session to update")
                .PageSize(15)
                .MoreChoicesText("[grey]Move up/down to see more sessions)[/]")
                .AddChoices(sessions)
                .UseConverter(session => GetSessionString(session!));
                // Using session! above to suppress compiler warning; not an issue since sessions are always non-null from GetAllSessions

            CodingSession selectedSession = AnsiConsole.Prompt(prompt);

            AnsiConsole.MarkupLine($"\n[yellow]Coding Session Selected:[/]\n[blue]{GetSessionString(selectedSession)}[/]\n");

            // Prompt for updates
            // Get start time (not required)
            var (startInput, newStart) = GetDateTimeInput("new start time", required: false);
            if (startInput.Trim().ToLower() == "q")
            {
                AnsiConsole.MarkupLine("[yellow]Operation cancelled.[/]");
                AnsiConsole.MarkupLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            // Get end time (not required)
            var (endInput, newEnd) = GetDateTimeInput("new end time", required: false);
            if (endInput.Trim().ToLower() == "q")
            {
                AnsiConsole.MarkupLine("[yellow]Operation cancelled.[/]");
                AnsiConsole.MarkupLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            CodingController.UpdateSession(selectedSession.Id, newStart, newEnd);
            AnsiConsole.MarkupLine("[green]Session updated successfully![/]");
            AnsiConsole.MarkupLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void DeleteSessionMenu()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[yellow]Delete a Coding Session[/]");

            var sessions = CodingController.GetAllSessions();
            if (!sessions.Any())
            {
                AnsiConsole.MarkupLine("[red]No sessions to delete![/]");
                AnsiConsole.MarkupLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            var prompt = new SelectionPrompt<CodingSession>()
                .Title("Select a coding session to delete")
                .PageSize(15)
                .MoreChoicesText("[grey]Move up/down to see more sessions)[/]")
                .AddChoices(sessions)
                .UseConverter(session => GetSessionString(session!));
                // Using session! above to suppress compiler warning; not an issue since sessions are always non-null from GetAllSessions

            CodingSession selectedSession = AnsiConsole.Prompt(prompt);

            AnsiConsole.MarkupLine($"\n[yellow]Coding Session Selected:[/]\n[blue]{GetSessionString(selectedSession)}[/]\n");
            AnsiConsole.MarkupLine($"[red]WARNING: Once deleted, entries in the database cannot be recovered[/]\n");

            CodingController.DeleteSession(selectedSession.Id);

            AnsiConsole.MarkupLine("[green]Session deleted successfully![/]");
            AnsiConsole.MarkupLine("Press any key to continue...");
            Console.ReadKey();
        }

        // ------------------------------------------------------------------------------- //
        // --------------------------------- HELPER METHODS ------------------------------ //
        // ------------------------------------------------------------------------------- //

        private static (string Input, DateTime? DateTime) GetDateTimeInput(string fieldName, bool required)
        {
            string promptText = required
                ? $"Input [green]{fieldName}[/] (yyyy-MM-dd HH:mm) or 'q' to quit:"
                : $"Input [green]{fieldName}[/] (yyyy-MM-dd HH:mm), Enter to skip, or 'q' to quit:";

            DateTime? result = required ? DateTime.MinValue : null; // Default based on required

            var prompt = new TextPrompt<string>(promptText)
                .PromptStyle("green");

            if (!required)
                prompt.AllowEmpty();

            prompt.Validate(input =>
            {
                if (input.Trim().ToLower() == "q")
                    return ValidationResult.Success();

                if (!required && string.IsNullOrEmpty(input))
                    return ValidationResult.Success();

                DateTime temp; // Need to store this as a non-nullable type for TryParseDateTime

                bool isValid = InputValidation.TryParseDateTime(input, out temp);

                if (isValid)
                    result = temp; // Assign the dateTime value to a nullable result, only if the validation check passes

                if (!isValid && required)
                    return ValidationResult.Error("[red]Invalid format! Use 'yyyy-MM-dd HH:mm' (e.g., 2025-02-26 14:30)[/]");

                return isValid
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Invalid format or empty not allowed! Use 'yyyy-MM-dd HH:mm'[/]");
            });

            string input = AnsiConsole.Prompt(prompt);
            return (input, result);
        }

        private static string GetSessionString(CodingSession session)
        {
            return $"{session.Id}\t{session.StartTime:yyyy-MM-dd HH:mm}\t{(session.EndTime.HasValue ? session.EndTime.Value.ToString("yyyy-MM-dd HH:mm") : "")}\t{(session.Duration.HasValue ? session.Duration.Value.ToString(@"hh\:mm\:ss") : "")}";
        }
    }
}
