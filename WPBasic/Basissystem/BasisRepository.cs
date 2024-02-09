using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WPBasic.Interface;
using WPBasic.Logging;
using WPBasic.Logging.Model;

namespace WPBasic.Basissystem{
    public abstract class BasisRepository<T> : IRepository<T>{
    public string XmlFilePath;

    public List<T>? Get(){
        var serializer = new XmlSerializer(typeof(List<T>));
        try{
            using var fileStream = new FileStream(XmlFilePath, FileMode.Open);
            List<T> _tmp = serializer.Deserialize(fileStream) as List<T>;
            return _tmp;
        }catch (Exception ex){
                WriteError(ex.Message);
                return new List<T>();
        }
    }
    public void Add(T value){
        var serializer = new XmlSerializer(typeof(List<T>));
        try{
            using var fileStream = new FileStream(XmlFilePath, FileMode.Create);
            var entries = Get();
            if (entries != null){
                entries.Add(value);
                serializer.Serialize(fileStream, entries);
            }else{
                WriteError($"Fehler beim abrufen des Verzeichnisses (Falscher Ordner?)", 2);
            }
        }catch (Exception ex){
            WriteError(ex.Message);
        }
    }
    public void Edit(T value){
        var serializer = new XmlSerializer(typeof(List<T>));
        try{
            using var fileStream = new FileStream(XmlFilePath, FileMode.Open);
            var entries = Get();
            if (entries != null){
                var index = entries.IndexOf(value);
                if (index >= 0){
                    entries[index] = value;
                    serializer.Serialize(fileStream, entries);
                }else{
                    entries.Add(value);
                    serializer.Serialize(fileStream, entries);
                    WriteError($"WorktimeRepository.Edit(WorktimeViewModel entry) mit nicht vorhanden Objekt im Verzeichnis aufgerufen", 1);
                }
            }else{
                WriteError($"Fehler beim abrufen des Verzeichnisses (Falscher Ordner?)", 2);
            }
        }catch (Exception ex){
            WriteError(ex.Message);
        }
    }
    public void Delete(string value){
        var serializer = new XmlSerializer(typeof(List<T>));
        try{
            using var fileStream = new FileStream(XmlFilePath, FileMode.Open);
            var entries = Get();
            if (entries != null){
                var index = entries.FindIndex(e => e.ToString() == value);
                if (index >= 0){
                    entries.RemoveAt(index);
                    serializer.Serialize(fileStream, entries);
                }else{
                    WriteError($"Objekt nicht im Verzeichnis : WorktimeRepository.Delete({value})", 2);
                }
            }else{
                WriteError($"Fehler beim abrufen des Verzeichnisses (Falscher Ordner?)", 2);
            }
        }catch (Exception ex){
            WriteError(ex.Message);
        }
    }
    public void WriteError(string error, int lvl = 1){
        ErrorLevel _lvl = lvl switch{
            1 => ErrorLevel.Info,
            2 => ErrorLevel.Warnung,
            3 => ErrorLevel.Error,
            _ => ErrorLevel.Error,
        };
        Log.AddLog(error, _lvl);
    }
    }
}