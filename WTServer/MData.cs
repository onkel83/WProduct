
namespace WTServer;


public enum Command{
    Login,
    Logout,
    Exit,
    Message
} 
[Serializable()]
public class MData : WPBasic.Basissystem.BasisModel
{
    private Command _Cmd;
    private string _Value;

    public Command Cmd{get => _Cmd; set => _Cmd = value;}
    public string Value{get => _Value; set => _Value = value;} 
}