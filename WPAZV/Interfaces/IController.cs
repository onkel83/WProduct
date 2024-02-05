using WPBasic.Logging;

namespace WPAZV;

public interface IController
{
    void Action(string action = "view", string data = "0"); 
    void Add();
    void Delete(int id);
    void Edit(int id);
    void View();
    void WriteLog(string msg, ErrorLevel lvl);
}

public abstract class BController : IController{
    public abstract void Action(string action = "view", string data = "0");
    public abstract void Add();
    public abstract void Delete(int id);
    public abstract void Edit(int id);
    public abstract void View();
    public void WriteLog(string msg = "", ErrorLevel lvl = ErrorLevel.Error){
        Log.AddLog(msg, lvl);
        Console.WriteLine($"Error in Worktime.Delete : {msg}");
    }
}
