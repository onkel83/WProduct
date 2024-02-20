using WPBasic;
using WPBasic.Helper.FileHelper;

namespace WTServer;

public class VMWorkTime : BasisViewModel<MWorkTime>
{
    public XmlFileHelper<MWorkTime> _Values = new ();

    public VMWorkTime(){
        _Values.FileHandler = Settings.GetSetting("DataPath") + Settings.GetSetting("WTData");
    }
    public void Save(){
        _Values.Storage = Values;
        _Values.Save();
    }
    public void Load(){
        _Values.Load();
        Values = _Values.Storage;
    }
}
