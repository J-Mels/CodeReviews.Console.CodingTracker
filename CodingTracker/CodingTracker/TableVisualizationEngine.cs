using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace CodingTracker
{
    public class TableVisualizationEngine
    {
        public static void DisplaySessions()
        {
            var table = new Table();

            table.AddColumn(new TableColumn("Id").Centered());
            table.AddColumn("Start Time");
            table.AddColumn("End Time");
            table.AddColumn(new TableColumn("Duration").Centered());

            foreach (var session in CodingController.GetAllSessions())
            {
                table.AddRow(
                    session.Id.ToString(),
                    session.StartTime.ToString("yyyy-MM-dd HH:mm"),
                    session.EndTime?.ToString("yyyy-MM-dd HH:mm") ?? "[grey]N/A[/]",
                    session.Duration.HasValue ? $"[green]{session.Duration.Value.ToString(@"hh\:mm\:ss")}[/]" : "[grey]N/A[/]"
                    );
            }

            AnsiConsole.Write(table);
        }
    }
}
