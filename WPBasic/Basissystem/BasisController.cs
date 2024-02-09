using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPBasic.Interface;
using WPBasic.Logging;
using WPBasic.Logging.Model;

namespace WPBasic.Basissystem
{
    public abstract class BasisController : IController
    {
        public abstract void Action(string action = "view", string data = "0");
        public abstract void Add();
        public abstract void Delete(int id);
        public abstract void Edit(int id);
        public abstract void View();
        public void WriteLog(string msg = "", ErrorLevel lvl = ErrorLevel.Error){
            Log.AddLog(msg, lvl);
            Console.WriteLine($"Error in Worktime.Delete : {msg}");
        }
    }
}