using WPBasic.Basissystem;
using WPBasic;

namespace WTServer
{
    public class UserController : BasisController
    {
        private VMUser _VM;
        private string _data;
        public UserController(){
            _VM = new();
            _data = string.Empty;
        }

        public override void Add(){
            _VM.Load();
            string[] tmp = _data.Split(';');
            _VM.Value = new MUser{
                ID = Convert.ToInt32(tmp[0]),
                UserName = tmp[1],
                Key = tmp[2]
            };
            _VM.Values.Add(_VM.Value);
            _VM.Save();
        }
        public override void Action(string action = "view", string data = "0")
        {
            if(data != "0"){
                _data = data;
                switch(action.ToLower()){
                    case "add" :
                        Add();
                        break;
                    case "edit" :
                        Edit();
                        break;
                    case "delete" :
                        Delete();
                        break;
                    case "view" :
                        View();
                        break;
                    case "login" :
                        Login();
                        break;
                    case "logout" :
                        Logout();
                        break;
                }
            }else{
                WriteLog($"Falscher Aufruf von Action(String, String) : Action({action}, {data})");
            }
        }
        public bool CheckLogin(){
            return (Settings.GetSetting("Login") != "1")?false:true;
        }
        public void Login(){
            Random rnd = new();
            string[] tmp = _data.Split(';');
            MUser user = new MUser{
                ID = rnd.Next(0,300),
                UserName = tmp[1],
                Key = tmp[2]
            };
            _VM.Load();
            foreach(MUser u in _VM.Values){
                if(u.UserName.Equals(user.UserName) && u.Key.Equals(user.Key)){
                    Settings.SetSetting("Login", "1");
                    break;
                }
            }
        }
        public void Logout(){
            Settings.SetSetting("Login", "0");
        }
        public override void Delete(int id = 0)
        {
            _VM.Load();
            foreach(MUser u in _VM.Values){
                if(u.ID == Convert.ToInt32(_data)){
                    _VM.Value = u;
                    break;
                }
            }
            _VM.Values.Remove(_VM.Value);
            _VM.Save();
        }
        public override void Edit(int id = 0)
        {
            string[] tmp = _data.Split(';');
            _VM.Value = new MUser{
                ID = Convert.ToInt32(tmp[0]),
                UserName = tmp[1],
                Key = tmp[2]
            };
            _data = _VM.Value.ID + "";
            Delete();
            _data = _VM.Value.ToString();
            Add();
        }
        public override void View()
        {
            _VM.Load();
            foreach(MUser u in _VM.Values){
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

    }
}