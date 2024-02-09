#pragma warning disable CS8601, CS8602, CS8604
using WPAZV.Repository;
using WPAZV.ViewModel;
using WPBasic.Logging;
using WPBasic.Logging.Model;

namespace WPAZV.Controller 
{
    public class EmployeeController
    {
        private readonly EmployeeRepository _repository;

        public EmployeeController(string xmlFilePath)
        {
            _repository = new EmployeeRepository(xmlFilePath);
        }

        public void _Add()
        {
            var entry = new EmployeeViewModel();
            Console.Write("Nachname: ");
            entry.LastName = Console.ReadLine();
            Console.Write("Vorname: ");
            entry.FirstName = Console.ReadLine();
            Console.Write("Geburtsdatum : ");
            entry.Birthday = DateTime.Parse(Console.ReadLine());
            Console.Write("Telefon Nr : ");
            entry.PhoneNumber = Console.ReadLine();
            Console.Write("Geschlecht (M/W/D): ");
            entry.Gender = Console.ReadKey().KeyChar;
            try{
                entry.ID = Convert.ToInt32(_repository.Get().OrderBy(u => u.ID).LastOrDefault().ID) + 1;
                _repository.Add(entry);
                Console.WriteLine("Worktime entry added successfully.");
            }catch(Exception ex){
                Log.AddLog(ex.Message, ErrorLevel.Error);
                Console.WriteLine($"Error in EmployeeController.Add : {ex.Message}");
            }
        }

        public void _Edit(int id)
        {
            var entry = _repository.Get().Find(e => e.ID == id);
            if (entry != null)
            {
                
                Console.Write("Nachname : ");
                entry.LastName = Console.ReadLine();
                Console.Write("Vorname: ");
                entry.FirstName = Console.ReadLine();
                Console.Write("Geburstag: ");
                entry.Birthday = DateTime.Parse(Console.ReadLine());
                Console.Write("Telefon: ");
                entry.PhoneNumber = Console.ReadLine();
                Console.Write("Geschlecht (M/W/D) : ");
                entry.Gender = Convert.ToChar(Console.ReadKey());
                try{
                    _repository.Edit(entry);
                    Console.WriteLine("Worktime entry updated successfully.");
                }catch(Exception ex){
                    Log.AddLog(ex.Message, ErrorLevel.Error);
                    Console.WriteLine($"Error in EmployeeController.Edit : {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Worktime entry not found.");
            }
        }

        public void _Delete(int id)
        {
            var entry = _repository.Get().Find(e => e.ID == id);
            if (entry != null)
            {
                try{
                    _repository.Delete(id.ToString());
                    Console.WriteLine("Employee deleted successfully.");
                }catch(Exception ex){
                    Log.AddLog(ex.Message, ErrorLevel.Error);
                    Console.WriteLine($"Error in EmployeeController.Delete : {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Worktime entry not found.");
            }
        }

        public void _View()
        {
            var entries = _repository.Get();
            foreach (var entry in entries)
            {
                Console.WriteLine($"ID: {entry.ID}");
                Console.WriteLine($"Nachname: {entry.LastName}");
                Console.WriteLine($"Vorname: {entry.FirstName}");
                Console.WriteLine($"Geburstag: {entry.Birthday}");
                Console.WriteLine($"Telefon: {entry.PhoneNumber}");
                Console.WriteLine($"Geschlecht: {entry.Gender}");
            }
        }

        public void Action(string view = "view", string data = "0"){
            switch(view.ToLower()){
                default : _View();break;
            }
        }
    }
}
#pragma warning restore CS8601, CS8602, CS8604