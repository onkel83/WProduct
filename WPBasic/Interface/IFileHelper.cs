using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPBasic.Logging.Model;

namespace WPBasic.Interface
{
    public interface IFileHelper<T>
    {
        #region Public Member
        string FileHandler{get;set;}
        List<T> Storage {get;set;}
        #endregion
        #region Public Methods
        void Load();
        void Save();
        void WriteToLog(string msg, ErrorLevel lvl);
        #endregion
    }
}