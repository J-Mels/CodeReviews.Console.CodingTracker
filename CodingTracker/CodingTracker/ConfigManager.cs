using System.Text.Json;

namespace CodingTracker
{
    public static class ConfigManager
    {
        public static string? DatabasePath { get; private set; }
        public static string? ConnectionString { get; private set; }

        static ConfigManager()
        {
            LoadConfig();
        }

        private static void LoadConfig()
        {
            string configFile = "config.json";

            if (!File.Exists(configFile))
            {
                throw new FileNotFoundException("Configuration file not found!");
            }

            string json = File.ReadAllText(configFile);
            var config = JsonSerializer.Deserialize<ConfigData>(json);

            if (config == null || config.Database == null)
            {
                throw new Exception("Invalid configuration file format.");
            }

            DatabasePath = config.Database.Path;
            ConnectionString = config.Database.ConnectionString;
        }

        private class ConfigData
        {
            public DatabaseConfig? Database { get; set; }
        }

        private class DatabaseConfig
        {
            public string? Path { get; set; }
            public string? ConnectionString { get; set; }
        }
    }
}
