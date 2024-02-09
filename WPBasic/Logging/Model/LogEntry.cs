using WPBasic.Logging.Interfaces;

namespace WPBasic.Logging.Model{
    public enum ErrorLevel{
        Info,
        Error,
        Warnung
    }
    public class LogEntry : ILogEntry{
        #region Public Members
        public int ID {get;set;}
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public ErrorLevel Level { get; set; }
        #endregion
        public LogEntry(int id, DateTime date, string message, ErrorLevel level){
            ID = id;
            Date = date;
            Message = message;
            Level = level;
        }
    }
}