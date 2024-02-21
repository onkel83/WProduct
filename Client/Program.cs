using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WPBasic;
using WTServer;
using WPBasic.Helper;
using WPServClien.Models;
using WPServClien;
using WTServClien.Models;
using WTClient;

class UDPClient
{
    private static int serverPort = Convert.ToInt32(Settings.GetSetting("Port"));
    private static string serverIp = Settings.GetSetting("ServerIP"); // Change this to the actual IP address of your server
    private static ConsolClient _client = new();
    static void Main()
    {
        if(!File.Exists("/var/Data/settings.xml")){
            Console.WriteLine($"Bitte geben sie ihren UserNamen ein : ");
            string user = Console.In.ReadLine();
            Console.WriteLine($"Bitte geben sie ihr Kennwort ein : ");
            string pass = Console.In.ReadLine();
            Settings.SetSetting("Port","8080");
            Settings.SetSetting("ServerIP", "127.0.0.1");
            Settings.SetSetting("UserID", "1");
            Settings.SetSetting("PWD",pass);
            Settings.SetSetting("User",user);
        }
        _client.ShowMainMenue();
        /*using (var client = new UdpClient())
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
        }*/
    } 
}
