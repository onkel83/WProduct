using System.Net;
using System.Net.Sockets;
using System.Text;
using WPBasic;
using WPBasic.Logging;
using WPBasic.Logging.Model;
using WPServClien;
using WPServClien.Models;

namespace WTServer
{
    public class UDPServer{
        private int listenPort = Convert.ToInt32(Settings.GetSetting("Port"));
        private UserController _UC = new();
        private WorkTimeController _WTC = new();
        private bool _IsRunning = true;
        
        public UDPServer(){
            Console.WriteLine("UDP Server is running...");
            var server = new UdpClient(listenPort);
            var groupEP = new IPEndPoint(IPAddress.Any, listenPort);

            try{
                while (_IsRunning){
                    byte[] bytes = server.Receive(ref groupEP);
                    string dataReceived = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    Console.WriteLine($"Received: {dataReceived}");

                    // Deserialize the JSON string into a Data object
                    MData data = Newtonsoft.Json.JsonConvert.DeserializeObject<MData>(dataReceived);
                    
                    switch(data.Typus){
                        case Family.User :
                            ChooseUser(ref data);
                            break;
                        case Family.Data :
                            ChooseData(ref data);//Neue Sachen und Testfälle !
                            break;
                        case Family.WorkTime :
                            ChooseWorkTime(ref data);
                            break;
                    }
                CheckDebug($"Received ID: {data.ID},Typus: {data.Typus}, Command: {data.Cmd}, Value: {data.Value}");

                // Check if user wants to quit
                /*Console.WriteLine("Enter 'quit' to stop the server:");
                string input = Console.ReadLine().Trim();
                if (input.ToLower() == "quit")
                {
                    isRunning = false;
                }*/
                }
            }catch (SocketException ex){
                Log.AddLog($"SocketException: {ex.Message}", ErrorLevel.Error);
            }catch (Exception ex){
                Log.AddLog($"An error occurred: {ex.Message}", ErrorLevel.Error);
            }finally{
                server.Close();
            }
        }

        private void ChooseUser(ref MData mData){
            switch(mData.Cmd){
                case Command.Login :
                    CheckDebug("Received Login Command");
                    _UC.Action("Login", mData.Value);
                break;
                case Command.Logout :
                    CheckDebug("Received Logout Command");
                    _UC.Action("Logout", mData.Value);
                break;
                case Command.Add :
                    CheckDebug("Received Add Command");
                    _UC.Action("Add", mData.Value);
                break;
                case Command.Edit :
                    CheckDebug("Received Edit Command");
                    _UC.Action("Edit", mData.Value);
                break;
                case Command.Delete :
                    CheckDebug("Received Delete Command");
                    _UC.Action("Delete", mData.Value);
                break;
                case Command.Get :
                    CheckDebug("Received Get Command");
                    _UC.Action("View", "all");
                break;
                case Command.Exit :
                    _IsRunning = false;    
                    CheckDebug("Received Exit Command");
                break;
                default :
                    CheckDebug($"Received unknown Command : {mData.Cmd}");
                break;
            }

        }
        private void ChooseWorkTime(ref MData mData){
            switch(mData.Cmd){
                case Command.Add:
                    _WTC.Action("add", mData.Value);
                    break;
                case Command.Edit:
                    _WTC.Action("edit", mData.Value);
                    CheckDebug("Received Edit Command");
                    break;
                case Command.Delete:
                    _WTC.Action("delete", mData.Value);
                    CheckDebug("Received Delete Command");
                    break;
                case Command.Get:
                    _WTC.Action("view", "all");
                    CheckDebug("Received Get Command");
                    break;
                case Command.Exit:
                    _IsRunning = false;
                    CheckDebug("Received Exit Command");
                    break;
                default:
                    CheckDebug($"Received unknown Command : {mData.Cmd}");
                    break;
            }
        }
        private void ChooseData(ref MData mData){
            switch(mData.Cmd){
                case Command.Login :
                    _UC.Action("view");
                    break;
            }
        }
        private void CheckDebug(string msg){
            if(Settings.GetSetting("Debug") == "true"){
                Log.AddLog(msg, ErrorLevel.Info);
            }else{
                Console.WriteLine($"{msg}");
            }
        }
    }
}
