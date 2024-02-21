using WPBasic.Basissystem;
using WPServClien.ViewModels;
using WTServClien.Models;

namespace WTClient;

public class UserController : BasisController
{
    private VMUser _VM;
    private string _data;
    public string Data{get=>(_data != null)?_data:""; private set => _data = value;}
    public UserController(){
        _VM = new();
        _data = string.Empty;
    }
    public override void Action(string action = "view", string data = "0")
    {
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
            default:
                CheckDebug($"Unbekannter Befehl : {action}");
                break;
        }
    }

    public override void Add()
    {
        _VM.Load();
        Console.Clear();
        Console.WriteLine("Bitte geben sie ihren Nutzernamen ein : ");
        string user = Console.In.ReadLine();
        Console.WriteLine("Bitte geben sie ihr Passwort ein : ");
        string pass = Console.In.ReadLine();
        _VM.Value = new (){
            ID = _VM.Values.Count + 1,
            UserName = (!string.IsNullOrEmpty(user))?user:"test",
            Key = (!string.IsNullOrEmpty(pass))?pass:_VM.Value.UserName
        };
        _VM.Values.Add(_VM.Value);
        _VM.Save();
    }

    public override void Delete(int id = 0)
    {
        Console.WriteLine("Bitte die ID des Users zum Löschen eingeben : ");
        string Id = Console.In.ReadLine();
        _VM.Load();
        _VM.Value = new (){
            ID = Convert.ToInt32(Id)
        };
        foreach(MUser u in _VM.Values){
            if(u.ID == _VM.Value.ID){
                _VM.Values.Remove(u);
                break;
            }
        }
        _VM.Save();
    }

    public override void Edit(int id = 0)
    {
        _VM.Load();
        Console.WriteLine("Geben Sie bitte die ID des zu bearbeitenden Users ein : ");
        string Id = Console.In.ReadLine();
        foreach(MUser u in _VM.Values){
            if(u.ID == Convert.ToInt32(Id)){
                Console.WriteLine($"BenuterName : {u.UserName}");
                Console.WriteLine($"Ändern ? (J/N)");
                if(Console.In.ReadLine() == "J"){
                    Console.WriteLine($"Neuer BenuterName : ");
                    u.UserName = Console.In.ReadLine();
                }
                Console.WriteLine($"Passwort : {u.Key}");
                Console.WriteLine($"Ändern ? (J/N)");
                if(Console.In.ReadLine() == "J"){
                    Console.WriteLine("Neues Password : ");
                    u.Key = Console.In.ReadLine();
                }
                break;
            }
        }
        _VM.Save();
    }

    public override void View()
    {
        Console.WriteLine("Möchten sie alle Einträge ansehen ? (J/N)");
        string result = Console.In.ReadLine();
        if(result == "N"){
            Console.WriteLine("Welche UserID möchten sie sehen ? (NR)");
            result = Console.In.ReadLine();
        }
        _VM.Load();
        foreach(MUser u in _VM.Values){
            if(!string.IsNullOrEmpty(result) && result.ToLower() != "j"){
                if(u.ID == Convert.ToInt32(result)){
                    Console.WriteLine($"{u.ToString()}");
                    break;
                }
            }else{
                Console.WriteLine($"{u.ToString()}");
            }
        }
    }
    private void Login(){
        Console.Clear();
        Console.WriteLine($"Bitte geben sie ihren Nutzernamen ein : ");
        string user = Console.In.ReadLine();
        Console.WriteLine("Bitte geben sie ihr Passwort ein : ");
        string pwd = Console.In.ReadLine();
        Console.Clear();
        _data = user+";"+pwd ;
    }
}
