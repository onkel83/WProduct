using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using WPBasic;
using WPBasic.Logging;
using WPBasic.Logging.Model;
using WPServClien;
using WPServClien.Models;

namespace WTClient
{
    public abstract class Client
    {
        private UserController? _UC;
        public UserController UC {get => (_UC != null)?_UC:new();set=>_UC = value;}
        public String UserID{get;set;}
    }
    public class ConsolClient : Client{
        private bool _IsRunning = true;
        private int _ServerPort;
        private string  _ServerIp ;// Change this to the actual IP address of your server

        public ConsolClient(){
            Settings.LoadFromFile("/var/Data/CS.xml");
            _ServerIp = Settings.GetSetting("ServerIP");
            _ServerPort = Convert.ToInt32(Settings.GetSetting("Port"));
            while(_IsRunning){
                ShowMainMenue();
            }
        }

        private void ShowMainMenue()
        {
            Console.Clear();
            Console.WriteLine("##############################"); //30
            Console.WriteLine("##        MainMenue         ##"); //30-4-MainMenue(9)
            Console.WriteLine("##############################"); //30
            Console.WriteLine("## 1      User Menue        ##"); //30-4-User Menue(10)
            Console.WriteLine("## 2      Arbeitszeit Menue ##");
            Console.WriteLine("## 3      Daten Senden      ##");
            Console.WriteLine("## 4      Beenden           ##");
            Console.WriteLine("##############################");
            Console.WriteLine("## Ihre Eingabe Bitte :     ##");
            switch(Console.In.ReadLine()){
                case "1" :
                    ShowUser(); 
                    break;
                case "2" :
                    ShowWorkTime();
                    break;
                case "3" :
                    ShowDaten();
                    break;
                case "4" :
                    Exit();
                    break;
                default :
                    Log.AddLog($"Unbekannte Eingabe !", ErrorLevel.Info);
                    break;
            }
        }

        private void Login(){
            
        }
        private void ShowUser(){
            Console.Clear();
            Console.WriteLine("##############################");
            Console.WriteLine("##        User Menue        ##");
            Console.WriteLine("##############################"); //30
            Console.WriteLine("## 1    Benutzer hinzufügen ##");
            Console.WriteLine("## 2    Benutzer bearbeiten ##");
            Console.WriteLine("## 3    Benutzer Löschen    ##");
            Console.WriteLine("## 4    Benutzer anzeigen   ##");
            Console.WriteLine("##                          ##");
            Console.WriteLine("## 9    zurück              ##");
            Console.WriteLine("##############################"); //30
            Console.WriteLine("## Ihre Auswahl :           ##");
            switch(Console.In.ReadLine()){
                case "1" :
                    UC.Action("add");
                    break;
                case "2" :
                    UC.Action("edit");
                    break;
                case "3" :
                    UC.Action("delete");
                    break;
                case "4" :
                    UC.Action("view");
                    Console.WriteLine($"Drücken sie [Enter] für Weiter !");
                    Console.In.ReadLine();
                    break;
                case "9" :
                    break;
                default:
                    Log.AddLog($"Unbekannte Eingabe !", ErrorLevel.Info);
                    break;
            }
        }
        private void ShowWorkTime(){
            Console.Clear();
            Console.WriteLine("##############################"); //30
            Console.WriteLine("##    Arbeitszeit Menue     ##");
            Console.WriteLine("##############################");
            Console.WriteLine("## 1 Arbeitszeit hinzufügen ##");
            Console.WriteLine("## 2 Arbeitszeit bearbeiten ##");
            Console.WriteLine("## 3 Arbeitszeit löschen    ##");
            Console.WriteLine("## 4 Arbeitszeit Anzeigen   ##");
            Console.WriteLine("## 9 zurück                 ##");
            Console.WriteLine("##############################");
            Console.WriteLine("## Ihre Auswahl :           ##");
            switch(Console.In.ReadLine()){
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "9":
                    break;
                default :
                    Log.AddLog($"Unbkennater Befehl !", ErrorLevel.Info);
                    break;
            }
        }
        private void ShowDaten(){
            Console.WriteLine("##############################"); //30
            Console.WriteLine("##        Daten Menue       ##"); //30
            Console.WriteLine("##############################"); //30
            Console.WriteLine("## 1      Daten Senden      ##"); //30
            Console.WriteLine("## 2      Daten Empfangen   ##"); //30
            Console.WriteLine("##                          ##"); //30
            Console.WriteLine("##                          ##"); //30
            Console.WriteLine("## 9      zuRück            ##"); //30
            Console.WriteLine("##############################"); //30
            Console.WriteLine("## Ihre Auswahl :           ##"); //30
            switch(Console.In.ReadLine()){
                case "1" :
                    Console.WriteLine($"Noch nicht hinzugefügt !");
                    break;
                case "2" :
                    Console.WriteLine($"Noch nicht hinzugefügt");
                    break;
                case "9" :
                    break;
                default :
                    Log.AddLog($"Unerlaubter zugriff !!!", ErrorLevel.Error);
                    break;
            }
        }
        private void Exit(){
            _IsRunning = false;
        }
        private void SendData(ref MData mData){
            using (var client = new UdpClient()){
                var groupEP = new IPEndPoint(IPAddress.Parse(_ServerIp), _ServerPort);
                try{
                    MData data = mData;
                    string json = JsonConvert.SerializeObject(data);
                    byte[] bytes = Encoding.UTF8.GetBytes(json);
                    client.Send(bytes, bytes.Length, groupEP);

                    byte[] receiveBytes = client.Receive(ref groupEP);
                    string receivedJson = Encoding.UTF8.GetString(receiveBytes);
                    MData receivedData = JsonConvert.DeserializeObject<MData>(receivedJson);
                    UC.CheckDebug($"Received: ID: {receivedData.ID}, Typus: {receivedData.Typus}, Value: {receivedData.Value}");
                    EmpfData(ref receivedData);
                    /*
                    UC.CheckDebug("UDP Client is running...");
                    bool _isRunning = true;
                    while (_isRunning){
                        MData sendData = mData;
                        switch(sendData.Cmd){
                            default :
                                ShowMainMenue();
                                break;
                        }
                        var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(sendData);
                        byte[] bytes = Encoding.UTF8.GetBytes(jsonData);

                        client.Send(bytes, bytes.Length, serverEndpoint);
                        UC.CheckDebug($"Sent data to server: {jsonData}");
                        _IsRunning = false;
                    }*/

                }catch (SocketException ex){
                    Console.WriteLine($"SocketException: {ex.Message}");
                }catch (Exception ex){
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }finally {
                    client.Close();   
                }
            }
        }
        private void EmpfData(ref MData mData){
            switch(mData.Typus){
                case Family.Data :
                    ResponseData(ref mData);
                    break;
                case Family.User :
                    break;
                case Family.WorkTime :
                    break;
                default :
                    break;
            }
        }
        private void ResponseData(ref MData mData){
            string[] tmp = mData.Value.Split(';');
            switch(mData.Cmd){
                case Command.Login :
                    if(tmp[0] == "true"){
                        UserID = tmp[1];
                    }else{
                        UC.CheckDebug($"Fehler hafter Login Versuch ! {mData.Value}");
                    }
                    break;
                case Command.Logout :
                    UserID = "0";
                    break;
                default :
                    break;
            }
        }
    }
}