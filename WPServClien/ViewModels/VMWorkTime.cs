using WPBasic;
using WPBasic.Helper.FileHelper;
using WTServClien.Models;

namespace WPServClien.ViewModels{

public class VMWorkTime : BasisViewModel<MWorkTime>
{
        public XmlFileHelper<MWorkTime> _Values = new ();

        public VMWorkTime(){
            Value = new();
            Values = new();
            _Values.FileHandler = Settings.GetSetting("VMWorkTime");
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
}