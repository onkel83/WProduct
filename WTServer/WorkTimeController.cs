using WPBasic.Basissystem;
using WPBasic.Logging.Model;
using WPServClien.ViewModels;
using WTServClien.Models;

namespace WTServer
{
    public class WorkTimeController : BasisController
    {
        private VMWorkTime _VM;
        private string _data;
        public WorkTimeController(){
            _VM = new();
            _data = string.Empty;
        }

        public override void Action(string action = "view", string data = "0")
        {
            _data = data;
            switch(action.ToLower()){
                case "add" :
                Add(); //Aufruf Action("add", "id;userid;start;stop;pause");
                break;
                case "edit" :
                Edit(); //Aufruf Action("edit", "oldid;userid;start;stop;pause");
                break;
                case "delete" :
                Delete(); //Aufruf Action("delete", id);
                break;
                case "view" :
                View(); //Aufruf Action("view", "all");
                break;
                default :
                    WriteLog($"Falscher aufruf von Action(string, string) : Action({action}, {data})", ErrorLevel.Error);
                break;
            }
        }

        public override void Add()
        {
            _CreateValue();
            _VM.Load();
            _VM.Values.Add(_VM.Value);
            _VM.Save();
        }

        public override void Delete(int id = 0)
        {
            _VM.Load();
            _VM.Value = new (){
                ID = Convert.ToInt32(_data)
            };
            foreach(MWorkTime wt in _VM.Values){
                if(wt.ID == _VM.Value.ID){
                    _VM.Value = wt;
                    break;
                }
            }
            _VM.Values.Remove(_VM.Value);
            _VM.Save();
        }

        public override void Edit(int id = 0)
        {
            _CreateValue();
            _VM.Load();
            _data = _VM.Value.ID + "";
            Delete();
            _VM.Values.Add(_VM.Value);
            _VM.Save();
        }

        public override void View()
        {
            _VM.Load();
            foreach(MWorkTime u in _VM.Values){
                if(_data.ToLower() == "all" || string.IsNullOrEmpty(_data))
                    Console.WriteLine($"{u.ToString()}");
                else{
                    if(u.ID == Convert.ToInt32(_data)){
                        Console.WriteLine($"{u.ToString()}");
                        break;
                    }
                }
            }
        }

        private void _CreateValue(){
            string[] tmp = _data.Split(';');
            _VM.Value = new(){
                ID = Convert.ToInt32(tmp[0]),
                UserID = tmp[1],
                Start = tmp[2],
                Stop = tmp[3],
                Pause = tmp[4],
            };
        }
    }
}