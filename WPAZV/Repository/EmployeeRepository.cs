#pragma warning disable CS8602
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

        public List<Employee>? Get(string id ="0"){
            var serializer = new XmlSerializer(typeof(List<Employee>));
            List<Employee> _tmp = new();
            using (var fileStream = new FileStream(_xmlFilePath, FileMode.Open)){
                foreach(Employee e in serializer.Deserialize(fileStream) as List<Employee>){
                    _tmp.Add(e);
                }
            }
            if(id != "0" && _tmp != null && _tmp.Count > 0){
                foreach(Employee e in _tmp){
                    if(e.ID + "" == id){
                        var result = new List<Employee>();
                        result.Add(e);
                        return result;
                    }
                }
            }
            return _tmp;
        }

        public void Add(Employee entry){
            var serializer = new XmlSerializer(typeof(List<Employee>));
            using (var fileStream = new FileStream(_xmlFilePath, FileMode.Create)){
                var entries = Get();
                if(entries != null){
                    entries.Add(entry);
                    serializer.Serialize(fileStream, entries);
                }
            }
        }

        public void Edit(Employee entry){
            var serializer = new XmlSerializer(typeof(List<Employee>));
            using (var fileStream = new FileStream(_xmlFilePath, FileMode.Open)){
                var entries = Get();
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
                var entries = Get();
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
#pragma warning restore CS8602