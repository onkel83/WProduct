/*
 * ##USAGE##
 * // Add a log entry with a message and a specific error level
 * Log.AddLog("Starting application", ErrorLevel.Info);
 * // Add another log entry with a warning message
 * Log.AddLog("Warning: Configuration file not found", ErrorLevel.Warning);
 * // Add an error message
 * Log.AddLog("Error: Database connection failed", ErrorLevel.Error);
 * // Get all log entries and print them to the console
 * foreach (LogEntry logEntry in Log.GetLogEntries())
 * {
 *    Console.WriteLine($"{logEntry.Date}: {logEntry.Level} - {logEntry.Message}");
 * }
 */
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using WPBasic.Logging.Interfaces;
using WPBasic.Logging.Model;

namespace WPBasic.Logging
{
    public static class Log
    {
        private static List<LogEntry> _logEntries = new();

        private static readonly string _logFilePath = Settings.GetSetting("Log")+".xml";

        public static void AddLog(string message, ErrorLevel level)
        {
            LogEntry logEntry = new(GetLogEntries().Count + 1, DateTime.UtcNow, message, level);
            _logEntries.Add(logEntry);

            // Save the log entry to the file
            SaveLogToFile();
        }

        public static List<LogEntry> GetLogEntries()
        {
            // Load the log entries from the file
            LoadLogFromFile();

            return _logEntries;
        }

        private static void SaveLogToFile(){
            // Serialize the log entries to XML
            using XmlTextWriter writer = new(_logFilePath, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;

            XmlSerializer serializer = new(typeof(List<LogEntry>));
            serializer.Serialize(writer, _logEntries);
        }


        private static void LoadLogFromFile()
        {
            if(File.Exists(_logFilePath)){
            // Deserialize the log entries from XML
            XmlTextReader reader = new(_logFilePath);
            XmlSerializer serializer = new(typeof(List<LogEntry>));
            #pragma warning disable CS8600, CS8601
            _logEntries = (List<LogEntry>)serializer.Deserialize(reader);
            #pragma warning restore CS8600, CS8601
            reader.Close();
            }else{
                _logEntries = new List<LogEntry>();
            }
        }
    }
}