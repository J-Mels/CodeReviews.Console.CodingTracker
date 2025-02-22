using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SQLite;

namespace CodingTracker
{
    static class CodingController
    {

        public static void CreateSession(DateTime start, DateTime? end = null)
        {
            var session = new CodingSession(start, end);

            using (var connection = new SQLiteConnection(ConfigManager.ConnectionString))
            {
                connection.Execute(
                    "INSERT INTO coding_tracker (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)",
                    session
                );
            }
        }

        public static List<CodingSession> GetAllSessions()
        {

            using (var connection = new SQLiteConnection(ConfigManager.ConnectionString))
            {
                var sql = "SELECT * FROM coding_tracker";

                var sessions = connection.Query<CodingSession>(sql).ToList();

                return sessions;
            }
        }

    }
}
