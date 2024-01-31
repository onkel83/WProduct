namespace WPBasic.Logging{
    public enum ErrorLevel{
        Info,
        Error,
        Warnung
    }
    public class LogEntry : Interface.ILogEntry{
        #region Public Members
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public ErrorLevel Level { get; set; }
        #endregion
        public LogEntry(DateTime date, string message, ErrorLevel level){
            Date = date;
            Message = message;
            Level = level;
        }
    }
}