using System.Xml.Linq;
using System.Xml.Serialization;
using WPAZV.Model;

namespace WPAZV.Repository
{
    public class EmployeeRepository
    {
        private readonly string _xmlFilePath;

        public EmployeeRepository(string xmlFilePath)
        {
            _xmlFilePath = xmlFilePath;
        }

        public List<Employee>? GetAll(){
            var serializer = new XmlSerializer(typeof(List<Employee>));
            using (var fileStream = new FileStream(_xmlFilePath, FileMode.Open)){
                return serializer.Deserialize(fileStream) as List<Employee>;
            }
        }

        public void Add(Employee entry){
            var serializer = new XmlSerializer(typeof(List<Employee>));
            using (var fileStream = new FileStream(_xmlFilePath, FileMode.Create)){
                var entries = GetAll();
                if(entries != null){
                    entries.Add(entry);
                    serializer.Serialize(fileStream, entries);
                }
            }
        }

        public void Edit(Employee entry){
            var serializer = new XmlSerializer(typeof(List<Employee>));
            using (var fileStream = new FileStream(_xmlFilePath, FileMode.Open)){
                var entries = GetAll();
                if(entries != null){
                    var index = entries.IndexOf(entry);
                    if (index >= 0){
                        entries[index] = entry;
                        serializer.Serialize(fileStream, entries);
                    }
                }
            }
        }

        public void Delete(int id){
            var serializer = new XmlSerializer(typeof(List<Employee>));
            using (var fileStream = new FileStream(_xmlFilePath, FileMode.Open)){
                var entries = GetAll();
                if(entries != null){
                    var index = entries.FindIndex(e => e.ID == id);
                    if (index >= 0){
                        entries.RemoveAt(index);
                        serializer.Serialize(fileStream, entries);
                    }
                }
            }
        }
    }
}