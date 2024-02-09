using WPBasic.Basissystem;
using WPBasic.Interface;
using WPBasic.Logging.Model;

namespace WPBasic.Logging.Interfaces
{
    public interface ILogEntry : IModel
    {
        DateTime Date {get;set;}
        string Message{get;set;}
        ErrorLevel Level {get;set;}
    }
}