#pragma warning disable CS8601, CS8602, CS8604
using WPBasic.Logging;
using WPAZV.Repository;
using WPAZV.ViewModel;

namespace WPAZV.Controller;

public class WorktimeController : IController
{
    private readonly WorktimeRepository _repository;

    public WorktimeController(string xmlFilePath)
    {
        _repository = new WorktimeRepository(xmlFilePath);
    }

    public void _Add()
    {
        WorktimeViewModel entry = new WorktimeViewModel
        {
            ID = (from r in _repository.GetAll() orderby r.ID select r.ID).Max() + 1
        };
        entry.UserID = int.Parse(Console.ReadLine());
        Console.Write("Einsatzort: ");
        entry.Einsatzort = Console.ReadLine();
        Console.Write("Startzeit: ");
        entry.Startzeit = DateTime.Parse(Console.ReadLine());
        Console.Write("Endzeit: ");
        entry.Endzeit = DateTime.Parse(Console.ReadLine());
        Console.Write("Pause: ");
        entry.Pause = decimal.Parse(Console.ReadLine());
        try{
            _repository.Add(entry);
            Console.WriteLine("Worktime entry added successfully.");
        }catch(Exception ex){
            Log.AddLog(ex.Message, ErrorLevel.Error);
            Console.WriteLine($"Error in WorktimeController.Add : {ex.Message}");
        }
    }

    public  void _Edit(int id)
    {
        var entry = _repository.GetAll().Find(e => e.ID == id);
        if (entry != null)
        {
            Console.Write("Einsatzort: ");
            entry.Einsatzort = Console.ReadLine();
            Console.Write("Startzeit: ");
            entry.Startzeit = DateTime.Parse(Console.ReadLine());
            Console.Write("Endzeit: ");
            entry.Endzeit = DateTime.Parse(Console.ReadLine());
            Console.Write("Pause: ");
            entry.Pause = decimal.Parse(Console.ReadLine());
            try{
                _repository.Edit(entry);
                Console.WriteLine("Worktime entry updated successfully.");
            }catch(Exception ex){
                Log.AddLog(ex.Message, ErrorLevel.Error);
                Console.WriteLine($"Error in Worktime.Edit : {ex.Message}");
            }
        }
        else{
            Console.WriteLine("Worktime entry not found.");
        }
    }

    public  void _Delete(int id)
    {
        var entry = _repository.GetAll().Find(e => e.ID == id);
        if (entry != null)
        {
            try{
            _repository.Delete(id);
            Console.WriteLine("Worktime entry deleted successfully.");
            }catch(Exception ex){
                Log.AddLog(ex.Message, ErrorLevel.Error);
                Console.WriteLine($"Error in Worktime.Delete : {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Worktime entry not found.");
        }
    }

    public  void _View()
    {
        var entries = _repository.GetAll();
        foreach (var entry in entries)
        {
            Console.WriteLine($"ID: {entry.ID}");
            Console.WriteLine($"UserID: {entry.UserID}");
            Console.WriteLine($"Einsatzort: {entry.Einsatzort}");
            Console.WriteLine($"Startzeit: {entry.Startzeit}");
            Console.WriteLine($"Endzeit: {entry.Endzeit}");
            Console.WriteLine($"Pause: {entry.Pause}");
            Console.WriteLine($"Arbeitszeit: {entry.Arbeitszeit}");
        }
    }

    public void Action(string action = "view", string data = "0"){
        switch(action.ToLower()){
            case "add" : _Add();break;
            case "del" : _Delete(Convert.ToInt32(data));break;
            case "edit" : _Edit(Convert.ToInt32(data));break;
            case "view" : _View();break; 
            default: _View();break;
        }

    }
}
#pragma warning restore CS8601, CS8602, CS8604