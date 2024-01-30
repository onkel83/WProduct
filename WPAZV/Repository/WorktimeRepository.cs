using System.Xml.Serialization;
using WPAZV.Model;
using WPAZV.ViewModel;
using WPBasic.Logging;

namespace WPAZV.Repository
{
    public class WorktimeRepository{
        private readonly string _xmlFilePath;

        public WorktimeRepository(string xmlFilePath){
            _xmlFilePath = xmlFilePath;
        }

        public List<WorktimeViewModel> GetAll(){
            var serializer = new XmlSerializer(typeof(List<WorktimeViewModel>));
            try{
                using (var fileStream = new FileStream(_xmlFilePath, FileMode.Open)){
                    return serializer.Deserialize(fileStream) as List<WorktimeViewModel>;
                }
            }catch(Exception ex){
                new LogEntry(DateTime.Now, ex.Message, ErrorLevel.Error);
                return new List<WorktimeViewModel>();
            }
        }

        public void Add(WorktimeViewModel entry){
            var serializer = new XmlSerializer(typeof(List<WorktimeViewModel>));
            using (var fileStream = new FileStream(_xmlFilePath, FileMode.Create)){
                var entries = GetAll();
                if(entries != null){
                    entries.Add(entry);
                    serializer.Serialize(fileStream, entries);
                }
            }
        }

        public void Edit(WorktimeViewModel entry){
            var serializer = new XmlSerializer(typeof(List<WorktimeViewModel>));
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
            var serializer = new XmlSerializer(typeof(List<WorktimeViewModel>));
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