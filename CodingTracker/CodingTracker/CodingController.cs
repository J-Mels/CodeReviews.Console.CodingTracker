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
                string sql = "INSERT INTO coding_tracker (StartTime, EndTime) VALUES (@StartTime, @EndTime)";

                connection.Execute(sql, session);
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

        public static void UpdateSession(int sessionId, DateTime? newStart = null, DateTime? newEnd = null)
        {
            using (var connection = new SQLiteConnection(ConfigManager.ConnectionString))
            {
                if (!CheckSession(sessionId))
                {
                    throw new KeyNotFoundException($"Coding session with ID {sessionId} not found.");
                }

                string sqlSelect = "SELECT * FROM coding_tracker WHERE Id = @Id";

                var session = connection.QuerySingle<CodingSession>(sqlSelect, new { Id = sessionId });

                // ONLY UPDATE THE VALUES PROVIDED BY THE USER
                session.StartTime = newStart ?? session.StartTime;
                session.EndTime = newEnd ?? session.EndTime;

                string sqlUpdate = "UPDATE coding_tracker SET StartTime = @StartTime, EndTime = @EndTime WHERE Id = @Id";

                connection.Execute(sqlUpdate, new
                {
                    Id = sessionId,
                    StartTime = session.StartTime,
                    EndTime = session.EndTime,
                    Duration = session.Duration
                });

            }
        }

        public static void DeleteSession(int sessionId)
        {
            if (!CheckSession(sessionId))
            {
                throw new KeyNotFoundException($"Coding session with ID {sessionId} not found.");
            }

            using (var connection = new SQLiteConnection(ConfigManager.ConnectionString))
            {
                string sql = "DELETE FROM coding_tracker WHERE Id = @Id";

                connection.Execute(sql, new { Id = sessionId});
            }
        }

        private static bool CheckSession(int sessionId)
        {
            using (var connection = new SQLiteConnection(ConfigManager.ConnectionString))
            {
                string sql = "SELECT COUNT(*) FROM coding_tracker WHERE Id = @Id";
                int count = connection.ExecuteScalar<int>(sql, new { Id = sessionId });

                return count > 0;
            }

        }
    }
}
