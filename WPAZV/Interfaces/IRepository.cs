using System.Xml.Serialization;
using WPAZV.ViewModel;
using WPBasic;
using WPBasic.Logging;

namespace WPAZV.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T value);
        void Edit(T value);
        void Delete(string value);
        List<T>? Get();
    }

    public abstract class BRepository<T> : IRepository<T> {
        public string XmlFilePath;

        public List<T>? Get()
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            try
            {
                using var fileStream = new FileStream(XmlFilePath, FileMode.Open);
                return serializer.Deserialize(fileStream) as List<T>;
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                return new List<T>();
            }
        }
        public void Add(T value)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            try
            {
                using var fileStream = new FileStream(XmlFilePath, FileMode.Create);
                var entries = Get();
                if (entries != null)
                {
                    entries.Add(value);
                    serializer.Serialize(fileStream, entries);
                }
                else
                {
                    WriteError($"Fehler beim abrufen des Verzeichnisses (Falscher Ordner?)", 2);
                }
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
            }
        }
        public void Edit(T value)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            try
            {
                using var fileStream = new FileStream(XmlFilePath, FileMode.Open);
                var entries = Get();
                if (entries != null)
                {
                    var index = entries.IndexOf(value);
                    if (index >= 0)
                    {
                        entries[index] = value;
                        serializer.Serialize(fileStream, entries);
                    }
                    else
                    {
                        entries.Add(value);
                        serializer.Serialize(fileStream, entries);
                        WriteError($"WorktimeRepository.Edit(WorktimeViewModel entry) mit nicht vorhanden Objekt im Verzeichnis aufgerufen", 1);
                    }
                }
                else
                {
                    WriteError($"Fehler beim abrufen des Verzeichnisses (Falscher Ordner?)", 2);
                }
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
            }
        }
        public void Delete(string value)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            try
            {
                using var fileStream = new FileStream(XmlFilePath, FileMode.Open);
                var entries = Get();
                if (entries != null)
                {
                    var index = entries.FindIndex(e => e.ToString() == value);
                    if (index >= 0)
                    {
                        entries.RemoveAt(index);
                        serializer.Serialize(fileStream, entries);
                    }
                    else
                    {
                        WriteError($"Objekt nicht im Verzeichnis : WorktimeRepository.Delete({value})", 2);
                    }
                }
                else
                {
                    WriteError($"Fehler beim abrufen des Verzeichnisses (Falscher Ordner?)", 2);
                }
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
            }
        }

        public void WriteError(string error, int lvl = 1)
        {
            ErrorLevel _lvl = lvl switch
            {
                1 => ErrorLevel.Info,
                2 => ErrorLevel.Warnung,
                3 => ErrorLevel.Error,
                _ => ErrorLevel.Error,
            };
            Log.AddLog(error, _lvl);
        }
    }
}