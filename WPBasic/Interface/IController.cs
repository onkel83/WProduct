using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPBasic.Logging.Model;

namespace WPBasic.Interface
{
    public interface IController
    {
        void Action(string action = "view", string data = "0"); 
        void Add();
        void Delete(int id);
        void Edit(int id);
        void View();
        void WriteLog(string msg, ErrorLevel lvl);
    }
}