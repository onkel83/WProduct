using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using WPAZV.Model;

namespace WPAZV.Repository
{
    public class MitarbeiterRepository
    {
        private readonly List<Employee> _mitarbeiter;

        public MitarbeiterRepository()
        {
            _mitarbeiter = new List<Employee>();
        }

        public void Add(Employee mitarbeiter)
        {
            if (_mitarbeiter.Any(m => m.Gender == mitarbeiter.Gender && m.LastName == mitarbeiter.LastName && m.FirstName == mitarbeiter.FirstName))
            {
                throw new InvalidOperationException($"Es gibt bereits einen Employee mit dem gleichen Geschlecht, LastName und FirstName.");
            }

            _mitarbeiter.Add(mitarbeiter);
        }

        public void Edit(Employee mitarbeiter)
        {
            var existingMitarbeiter = _mitarbeiter.FirstOrDefault(m => m.ID == mitarbeiter.ID);

            if (existingMitarbeiter != null)
            {
                existingMitarbeiter.LastName = mitarbeiter.LastName;
                existingMitarbeiter.FirstName = mitarbeiter.FirstName;
                existingMitarbeiter.Birthday = mitarbeiter.Birthday;
                existingMitarbeiter.PhoneNumber = mitarbeiter.PhoneNumber;
                existingMitarbeiter.Gender = mitarbeiter.Gender;
            }
            else
            {
                throw new InvalidOperationException($"Employee mit der ID {mitarbeiter.ID} nicht gefunden.");
            }
        }

        public void Delete(int id)
        {
            var mitarbeiter = _mitarbeiter.FirstOrDefault(m => m.ID == id);

            if (mitarbeiter != null)
            {
                _mitarbeiter.Remove(mitarbeiter);
            }
            else
            {
                throw new InvalidOperationException($"Employee mit der ID {id} nicht gefunden.");
            }
        }

        public List<Employee> GetAll()
        {
            return _mitarbeiter;
        }

        public Employee GetByID(int id)
        {
            if(_mitarbeiter.FirstOrDefault(m => m.ID == id) != null)
                return _mitarbeiter.FirstOrDefault(m => m.ID == id);

            return new();
        }

        public void SaveToXml(string filePath)
        {
            XElement root = new XElement("Employee");

            foreach (var mitarbeiter in _mitarbeiter)
            {
                XElement mitarbeiterElement = new XElement("Employee",
                    new XElement("ID", mitarbeiter.ID),
                    new XElement("LastName", mitarbeiter.LastName),
                    new XElement("FirstName", mitarbeiter.FirstName),
                    new XElement("Birthday", mitarbeiter.Birthday.ToString("yyyy-MM-dd")),
                    new XElement("PhoneNumber",mitarbeiter.PhoneNumber),
                    new XElement("Geschlecht", mitarbeiter.Gender)
                );

                root.Add(mitarbeiterElement);
            }

            root.Save(filePath);
        }
    }
}