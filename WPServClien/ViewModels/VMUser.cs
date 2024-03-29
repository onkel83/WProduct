using WPBasic;
using WPBasic.Helper.FileHelper;
using WTServClien.Models;

namespace WPServClien.ViewModels{
    public class VMUser : BasisViewModel<MUser>{
        public BinaryFileHelper<MUser> _Values = new();

        public VMUser(){
            Value = new();
            Values = new();
            _Values.FileHandler = Settings.GetSetting("VMUser");
        }
        public void Load(){
            Values.Clear();
            _Values.Load();
            Values = _Values.Storage;
        }
        public void Save(){
            _Values.Storage.Clear();
            _Values.Storage = Values;
            _Values.Save();
        }       
    }
}