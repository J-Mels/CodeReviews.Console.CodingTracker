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
                DateTime temp; // Non-nullable needed here for TryParseDateTime
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
    }
}
