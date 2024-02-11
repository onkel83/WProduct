using WPBasic;
using WPBasic.Logging;
using WPBasic.Logging.Model;


Console.WriteLine($"This is a Test Entry !");

Console.ReadLine();
foreach(LogEntry e in Log.GetLogEntries()){
    Console.WriteLine($"{e.Level} :: {e.Date} :: {e.Message}");
}
Console.ReadLine();