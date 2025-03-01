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

            // have user input start time, and optionally end time, specifying the date-time format that should be used
            AnsiConsole.Ask<string>("Input coding session start time (yyyy-MM-dd HH:mm):"); 
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
