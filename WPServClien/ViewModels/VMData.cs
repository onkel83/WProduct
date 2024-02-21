using WPBasic;
using WPBasic.Helper.FileHelper;
using WPServClien.Models;

namespace WPServClient.ViewModels{
    public class VMData : BasisViewModel<MData>{
        public JsonFileHelper<MData> _Values = new ();

        public VMData(){
            Value = new();
            Values = new();
            _Values.FileHandler = Settings.GetSetting("VMData");
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