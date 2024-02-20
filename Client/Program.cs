using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WPBasic;
using WTServer;
using WPBasic.Helper;

class UDPClient
{
    private static int serverPort = Convert.ToInt32(Settings.GetSetting("Port"));
    private static string serverIp = Settings.GetSetting("ServerIP"); // Change this to the actual IP address of your server

    static void Main()
    {
        if(!File.Exists("settings.xml")){
            Console.WriteLine($"Bitte geben sie ihren UserNamen ein : ");
            string user = Console.In.ReadLine();
            Console.WriteLine($"Bitte geben sie ihr Kennwort ein : ");
            string pass = Console.In.ReadLine();
            WPBasic.Helper.Security.AES<string> aes = new WPBasic.Helper.Security.AES<string>(pass);
            Settings.SetSetting("Port","8080");
            Settings.SetSetting("ServerIP", "127.0.0.1");
            Settings.SetSetting("UserID", "1");
            Settings.SetSetting("PWD",aes.EncryptedMessage);
            Settings.SetSetting("User",user);
        }
        using (var client = new UdpClient())
        {
            string UserID = "0";
            var serverEndpoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
            bool send = false;
            try
            {
                
                Console.WriteLine("UDP Client is running...");
                ShowMenue();
                bool isRunning = true;
                while (isRunning)
                {
                    send = false;
                    string[] data = Console.ReadLine().Split('#');
                    MData sendData = new MData
                        {
                            ID = int.Parse(data[0]),
                            Typus = (Family)Enum.Parse(typeof(Family), data[1], true),
                            Cmd = (Command)Enum.Parse(typeof(Command), data[2], true),
                            Value = data[3]
                        };
                    switch(sendData.Cmd){
                        case Command.Login :
                            if(Login()){
                                UserID = Settings.GetSetting("UserID");
                            }else{
                                Console.WriteLine($"Falscher Username und/oder Kennung !");
                            }
                            break;
                        case Command.Logout :
                            if(UserID != "0"){
                                if(Logout()){
                                    UserID = "0";
                                }else{
                                    UserID = Settings.GetSetting("UserID");
                                }
                            }else{
                                Console.WriteLine($"Sie müssen eingeloggt sein !");
                            }
                            break;
                        case Command.Add :
                            if(UserID != "0"){
                                string tmp = Add();
                                if(tmp != null){
                                    sendData.Value = tmp;
                                    send = true;
                                }else{
                                    ShowMenue();
                                }
                            }else{
                                Console.WriteLine($"Sie müssen eingeloggt sein !");
                            }
                            break;
                        case Command.Edit :
                            if(UserID != "0"){
                                if(Edit(ref sendData)){
                                    send = true;
                                }else{
                                    ShowMenue();
                                }
                            }else{
                                Console.WriteLine($"Sie müssen eingeloggt sein !");
                            }
                            break;
                        case Command.Delete :
                            if(UserID != "0"){
                                if(Delete(ref sendData)){
                                    send = true;
                                }else{
                                    ShowMenue();
                                }
                            }else{
                                Console.WriteLine($"Sie müssen eingeloggt sein !");
                            }
                            break;
                        case Command.Get :
                            if(UserID != "0"){
                                Console.WriteLine($"Keine Ahnung muss ich wohl nen temp server für einbauen!");
                            }else{
                                Console.WriteLine($"Sie müssen eingeloggt sein !");
                            }
                            break;
                        case Command.Exit :
                            isRunning = false;
                            break;
                        default :
                            ShowMenue();
                            break;
                    }
                    if(send){
                        var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(sendData);
                        byte[] bytes = Encoding.UTF8.GetBytes(jsonData);

                        client.Send(bytes, bytes.Length, serverEndpoint);
                        Console.WriteLine($"Sent data to server: {jsonData}");
                    }
                    send = false;
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"SocketException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }
    } 
    private static void ShowMenue(){
        Console.Clear();
        Console.WriteLine($"#################################################################################################################");
        Console.WriteLine($"##    Main Menue :                                                                                             ##");
        Console.WriteLine($"#################################################################################################################");
        Console.WriteLine($"## 0) Login  (Format : 0#0#username;passwort)                                                                 ##");
        Console.WriteLine($"## 1) Logout (Format : 0#1#Exit)                                                                              ##");
        Console.WriteLine($"## 2) Add    (Format : ID#2#ID;UserID;Startzeit (hh:mm DD.MM.YYYY);Endzeit (hh:mm DD.MM.YYYY);Pause (Decimal)) ##");
        Console.WriteLine($"## 3) Edit   (Format : ID#3#NewID;NewUserID;NewStartzeit;NewEndzeit;NewPause)                                  ##");
        Console.WriteLine($"## 4) Delete (Format : ID#4#Delete)                                                                            ##");
        Console.WriteLine($"## 5) Get    (Format : ID#5#GetAll or WT.ID#5#Get)                                                             ##");
        Console.WriteLine($"## quit) Exit the Programm !                                                                                   ##");
        Console.WriteLine($"#################################################################################################################");
        Console.WriteLine($"Ihre Eingabe : ");
    }

    private static bool Login(){
        Console.Clear();
        Console.WriteLine($"Bitte gib den Usernamen ein : ");
        string user = Console.In.ReadLine().Trim();
        Console.WriteLine($"Gib bitte deine Kennung ein : ");
        string pwd = Console.In.ReadLine().Trim();
        WPBasic.Helper.Security.AES<string> aES = new WPBasic.Helper.Security.AES<string>(Settings.GetSetting("PWD"));
        string _tmp = aES.DecryptMessage();
        if(user != Settings.GetSetting("User") && pwd == _tmp){
            Settings.SetSetting("PWD", aES.EncryptedMessage);
            return true;
        }
        return false;
    }
    private static bool Logout(){
        Console.Clear();
        Console.WriteLine($"Sind sie Sicher ? J/N");
        string result = Console.In.ReadLine().ToUpper().Trim();
        return (result != "J")?false:true; 
            
    }
    private static string Add(){
        Console.Clear();
        MWorkTime result = new();
        Console.WriteLine($"Geben Sie bitte die ID ein : ");
        result.ID = Convert.ToInt32(Console.In.ReadLine());
        result.UserID = Settings.GetSetting("UserID");
        Console.WriteLine($"Geben Sie bitte Die Startzeit (Format : hh:mm dd.MM.yyyy) ein : ");
        result.Start = Console.In.ReadLine();
        Console.WriteLine($"Geben Sie bitte Die Endzeit   (Format : hh:mm dd.MM.yyyy) ein : ");
        result.Stop = Console.In.ReadLine();
        Console.WriteLine($"Geben Sie die Pausenzeit in H/decimal ein : ");
        result.Pause = Console.In.ReadLine();
        Console.Clear();
        Console.WriteLine($"Sie haben folgende Daten eingegeben : ");
        Console.WriteLine($"{result.ToString()}");
        Console.WriteLine($"Sind diese Daten korrekt? J/N");
        switch(Console.In.ReadLine()){
            case "J" :
                return result.ToString();
            default :
                return null;
        }
    }
    private static bool Edit(ref MData data){
        Console.WriteLine($"Welche ID möchten sie bearbeiten ? : ");
        data.ID = Convert.ToInt32(Console.In.ReadLine());
        MWorkTime wt = new();
        Console.WriteLine($"Geben Sie bitte die ID ein : ");
        wt.ID = Convert.ToInt32(Console.In.ReadLine());
        wt.UserID = Settings.GetSetting("UserID");
        Console.WriteLine($"Geben sie bitte die Startzeit ein (hh.mm dd.MM.YYYY)");
        wt.Start = Console.In.ReadLine();
        Console.WriteLine($"Geben sie bitte die Startzeit ein (hh.mm dd.MM.YYYY)");
        wt.Stop = Console.In.ReadLine();
        Console.WriteLine($"Geben sie bitte die Pausenzeit in decimal ein (1h = 1.0)");
        wt.Pause = Console.In.ReadLine();
        Console.Clear();
        Console.WriteLine($"Sind diese Angaben korrekt ? J/N");
        Console.WriteLine($"{wt.ToString()}");
        if(Console.In.ReadLine().ToUpper().Trim() != "J"){
            return false;
        }else{
            data.Value = wt.ToString();
            return true;
        }
    }
    private static bool Delete(ref MData data){
        Console.WriteLine($"Bitte geben sie die zu Löschende ID ein : ");
        string id = Console.In.ReadLine();
        Console.WriteLine("Sind sie sicher ? J/N");
        switch(Console.In.ReadLine().Trim().ToUpper()){
            case "J" :
                data.ID = Convert.ToInt32(id);
                return true;
            default :
                return false;
        }
    }
}
