using System;
using System.IO;
using System.Text.Json;

namespace CodingTracker
{
    public class ConfigManager
    {
        public string? DatabasePath { get; private set; }
        public string? ConnectionString { get; private set; }

        public ConfigManager()
        {
            LoadConfig();
        }

        private void LoadConfig()
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
