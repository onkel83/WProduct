using WPBasic;
using WPBasic.Helper.FileHelper;

namespace WTServer;

public class VMData : BasisViewModel<MData>
{
    public JsonFileHelper<MData> _Values = new ();

    public VMData(){
        _Values.FileHandler = Settings.GetSetting("DataPath") + Settings.GetSetting("VMData");
    }
    public void Save(){
        _Values.Storage = Values;
        _Values.Save();
    }
    public void Load(){
        _Values.Load();
        Values = _Values.Storage;
    }
    public void Show(){
        foreach(MData d in _Values.Storage){
            Console.WriteLine($"Typ : {d.Cmd}, Message : {d.Value}");
        }
    }
}
