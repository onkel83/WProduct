using WPBasic.Interface;

namespace WPWorker.Interfaces
{
    public interface IMWorker : IModel
    {
        string NName {get;set;}
        string VName {get;set;}
        string Phone {get;set;}
        string Gender {get;set;}
    }
}