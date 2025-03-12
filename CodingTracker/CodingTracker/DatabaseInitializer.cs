using Dapper;
using System.Data.SQLite;

namespace CodingTracker
{
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase()
        {
            if (!File.Exists(ConfigManager.DatabasePath))
            {
                SQLiteConnection.CreateFile(ConfigManager.DatabasePath);
            }

            using (var connection = new SQLiteConnection(ConfigManager.ConnectionString))
            {
                connection.Open();

                connection.Execute(@$"
                            CREATE TABLE IF NOT EXISTS 'coding_tracker' (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                StartTime TEXT NOT NULL,
                                EndTime TEXT
                                )");

                connection.Close();
            }
        }

    }
}
