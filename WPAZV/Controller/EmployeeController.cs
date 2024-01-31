#pragma warning disable CS8601, CS8602, CS8604
using WPAZV.Repository;
using WPAZV.ViewModel;

namespace WPAZV.Controller
{
    public class EmployeeController
    {
        private readonly EmployeeRepository _repository;

        public EmployeeController(string xmlFilePath)
        {
            _repository = new EmployeeRepository(xmlFilePath);
        }

        public void Add()
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
            entry.ID = Convert.ToInt32(_repository.GetAll().OrderBy(u => u.ID).LastOrDefault().ID) + 1;
            _repository.Add(entry);
            Console.WriteLine("Worktime entry added successfully.");
        }

        public void Edit(int id)
        {
            var entry = _repository.GetAll().Find(e => e.ID == id);
            if (entry != null)
            {
                /*
                Console.Write("Einsatzort: ");
                entry.Einsatzort = Console.ReadLine();
                Console.Write("Startzeit: ");
                entry.Startzeit = DateTime.Parse(Console.ReadLine());
                Console.Write("Endzeit: ");
                entry.Endzeit = DateTime.Parse(Console.ReadLine());
                Console.Write("Pause: ");
                entry.Pause = decimal.Parse(Console.ReadLine());
                _repository.Edit(entry);
                Console.WriteLine("Worktime entry updated successfully.");
                */
                Console.WriteLine("Hier würde Normalerweiße die Daten geändert werden!");
            }
            else
            {
                Console.WriteLine("Worktime entry not found.");
            }
        }

        public void Delete(int id)
        {
            var entry = _repository.GetAll().Find(e => e.ID == id);
            if (entry != null)
            {
                _repository.Delete(id);
                Console.WriteLine("Employee deleted successfully.");
            }
            else
            {
                Console.WriteLine("Worktime entry not found.");
            }
        }

        public void View()
        {
            var entries = _repository.GetAll();
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
    }
}
#pragma warning restore CS8601, CS8602, CS8604