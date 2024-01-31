using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPBasic.Logging;

namespace WPBasic.Interface
{
    public interface ILogEntry
    {
        DateTime Date {get;set;}
        string Message{get;set;}
        ErrorLevel Level {get;set;}
    }
}