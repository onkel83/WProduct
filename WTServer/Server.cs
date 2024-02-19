using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WPBasic;

namespace WTServer{
    public class UDPServer{
        private int listenPort = Convert.ToInt32(Settings.GetSetting("Port"));
        public UDPServer(){
        Console.WriteLine("UDP Server is running...");
        var server = new UdpClient(listenPort);
        var groupEP = new IPEndPoint(IPAddress.Any, listenPort);

        try{
            VMData vm = new VMData();
            VMWorkTime wt = new VMWorkTime();
            vm.Values = new();
            bool isRunning = true;
            while (isRunning){
                byte[] bytes = server.Receive(ref groupEP);
                string dataReceived = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                Console.WriteLine($"Received: {dataReceived}");

                // Deserialize the JSON string into a Data object
                MData data = Newtonsoft.Json.JsonConvert.DeserializeObject<MData>(dataReceived);
                
                // Perform actions based on the received Command
                switch (data.Cmd){
                    case Command.Login:
                        Console.WriteLine("Received Login command");
                        string[] _tmp = data.Value.Split(';');
                        Console.WriteLine(data.Value);
                        break;
                    case Command.Logout:
                        Console.WriteLine("Received Logout command");
                        
                        break;
                    case Command.Add:
                        Console.WriteLine("Received Add command");
                        vm.Values.Add(data);
                        vm.Save();
                        wt = new();
                        AddNew(data.Value);
                        break;
                    case Command.Edit:
                        Console.WriteLine("Received Edit command");
                        vm.Value = data;
                        foreach(MData m in vm.Values){
                            if(m.ID == vm.Value.ID){
                                vm.Values.Remove(m);
                                break;
                            }
                        }
                        vm.Values.Add(vm.Value);
                        vm.Save();
                        wt = new();
                        Edit(ref data);
                        break;
                    case Command.Delete:
                        Console.WriteLine("Received Delete command");
                        vm.Value.ID = data.ID;
                        foreach(MData m in vm.Values){
                            if(m.ID == vm.Value.ID){
                                vm.Values.Remove(m);
                                break;
                            }
                        }
                        vm.Save();
                        wt = new();
                        Delete(data);
                        break;
                    case Command.Get:
                        Console.WriteLine("Received Get command");
                        foreach(MData m in vm.Values){
                            Console.WriteLine($"ID : {m.ID}, Typ : {m.Cmd}, Value : {m.Value}");
                        }
                        Console.WriteLine($"Gespeicherte WorkTime Einträge : ");
                        Show();
                        break;
                    case Command.Exit:
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Unknown command received");
                        break;
                }
                Console.WriteLine($"Received ID: {data.ID}, Command: {data.Cmd}, Value: {data.Value}");

                // Check if user wants to quit
                /*Console.WriteLine("Enter 'quit' to stop the server:");
                string input = Console.ReadLine().Trim();
                if (input.ToLower() == "quit")
                {
                    isRunning = false;
                }*/
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
            server.Close();
        }
    }

        private void AddNew(string entry){
            string[] _tmp = entry.Split(';');
            VMWorkTime vm = new();
            MWorkTime wt = new MWorkTime{
                ID = Convert.ToInt32(_tmp[0]),
                UserID = _tmp[1],
                Start = _tmp[2],
                Stop = _tmp[3],
                Pause = _tmp[4]
            };
            vm.Values.Add(wt);
            vm.Save();
            Console.WriteLine($"Folgender Eintrag ist hinzugefügt worden :");
            Console.WriteLine($"ID            : {wt.ID}");
            Console.WriteLine($"UserID        : {wt.UserID}");
            Console.WriteLine($"Startzeit     : {wt.Start}");
            Console.WriteLine($"Endzeit       : {wt.Stop}");
            Console.WriteLine($"Pausen (in/h) : {wt.Pause}");
            Console.WriteLine($"Arbeitszeit Gesamt : {wt.Arbeitszeit}");
        }
        private MData GetAll(ref MData mData){
            string erg = "";
            VMWorkTime vm = new();
            vm.Load();
            foreach(MWorkTime _wt in vm.Values){
                erg += _wt.ToString();
                erg += "|";
            }
            erg.Substring(-1);
            mData.Cmd = Command.Get;
            mData.Value = erg;
            return mData;
        }
        private void Edit (ref MData mData){
            VMWorkTime vm = new();
            vm.Load();
            foreach(MWorkTime wt in vm.Values){
                if(wt.ID == mData.ID){
                    vm.Value = wt;
                    string[] _tmp = mData.Value.Split(';');
                    wt.ID = Convert.ToInt32(_tmp[0]);
                    wt.UserID = _tmp[1];
                    wt.Start = _tmp[2];
                    wt.Stop = _tmp[3];
                    wt.Pause = _tmp[4];
                    vm.Values.Remove(vm.Value);
                    vm.Values.Add(wt);
                    continue;
                }
            }
            vm.Save();
        }
        private void Delete(MData mData){
            VMWorkTime vm = new();
            vm.Load();
            foreach(MWorkTime wt in vm.Values){
                if(wt.ID == mData.ID){
                    vm.Values.Remove(wt);
                    break;
                }
            }
            vm.Save();
        }
        private MData GetOne(ref MData mData){
            MWorkTime result = new();
            string _erg = GetAll(ref mData).Value;
            string[] _tmp = _erg.Split('|');
            VMWorkTime wt = new ();
            foreach(string s in _tmp){
                string[] _a = s.Split(';');
                wt.Values.Add(new MWorkTime{
                    ID = Convert.ToInt32(_a[0]),
                    UserID = _a[1],
                    Start = _a[2],
                    Stop = _a[3],
                    Pause = _a[4]
                });
            }

            foreach(MWorkTime mWork in wt.Values){
                if(mWork.ID == mData.ID){
                    mData.Value = mWork.ToString();
                    continue;
                }
            }
            return mData;
        }
        private void Show(){
            VMWorkTime vm = new();
            vm.Load();
            foreach(MWorkTime wt in vm.Values){
                Console.WriteLine($"ID               :       {wt.ID}");
                Console.WriteLine($"UserID           :       {wt.UserID}");
                Console.WriteLine($"Start            :       {wt.Start}");
                Console.WriteLine($"Ende             :       {wt.Start}");
                Console.WriteLine($"Pause            :       {wt.Pause}");
                Console.WriteLine($"-----------------------------------");
                Console.WriteLine($"Auftrags Stunden : {wt.Arbeitszeit}");
                Console.WriteLine($":::::::::::::::::::::::::::::::::::");
            }
        }
    }
}
