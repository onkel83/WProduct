using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPBasic.Interface;
using WPBasic.Logging.Model;

namespace WPBasic.Basissystem
{
    public abstract class BaseFilehelper<T> : IFileHelper<T>
    {
        private string _FileHandler = "";
        private List<T> _Storage = new();

        public string FileHandler{
            get => _FileHandler;
            set => _FileHandler = value;
        }

        public List<T> Storage{
            get => _Storage;
            set => _Storage = value;
        }

        public abstract void Load();
        public abstract void Save();
        public void WriteToLog(string msg, ErrorLevel lvl = ErrorLevel.Error)
        {
            Logging.Log.AddLog(msg, lvl);
        }
    }
}