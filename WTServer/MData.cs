
namespace WTServer;


public enum Command{
    Login,
    Logout,
    Add,
    Edit,
    Delete,
    Get,
    Exit
}
public enum Family{
    User,
    Data,
    WorkTime
}
[Serializable()]
public class MData : WPBasic.Basissystem.BasisModel
{
    private Command _Cmd;
    private string _Value = string.Empty;
    private Family _Typus;

    public Family Typus{get => _Typus; set => _Typus = value;}
    public Command Cmd{get => _Cmd; set => _Cmd = value;}
    public string Value{get => _Value; set => _Value = value;} 
}