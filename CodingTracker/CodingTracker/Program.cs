using CodingTracker;
using System;

class Program
{
    static void Main()
    {
        try
        {
            ConfigManager configManager = new ConfigManager();
            Console.WriteLine($"Database Path: {configManager.DatabasePath}");
            Console.WriteLine($"Connection String: {configManager.ConnectionString}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load config: {ex.Message}");
        }
    }
}
