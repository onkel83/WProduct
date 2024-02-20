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
            CheckDebug($"Error in Worktime.Delete : {msg}", lvl);
        }
        public void CheckDebug(string msg, ErrorLevel lvl = ErrorLevel.Info){
            bool debug = (Settings.GetSetting("Debug") == "1")?true:false;
            switch(lvl){
                case ErrorLevel.Info:
                    if(debug){
                        Console.WriteLine(msg);
                    }else{
                        Log.AddLog(msg, lvl);
                    }
                    break;
                case ErrorLevel.Warnung:
                    if(debug){
                        Console.WriteLine(msg);
                    }
                    Log.AddLog(msg, lvl);
                    break;
                case ErrorLevel.Error:
                    if(debug){
                        Console.WriteLine(msg);
                    }
                    Log.AddLog(msg, lvl);
                break;
                default :
                    Console.WriteLine(msg);
                break;
            }
        }
    }
}