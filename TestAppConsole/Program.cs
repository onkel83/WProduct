using System.Runtime.Intrinsics.Arm;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using WPBasic;
using WPBasic.Helper.Security;
using WPBasic.Logging;
using WPBasic.Logging.Model;
using WTServer;

foreach (LogEntry e in Log.GetLogEntries()){
    Console.WriteLine($"{e.Level} :: {e.Date} :: {e.Message}");
}
Console.ReadLine();
/*
var Data = new { typ = "login" ,message = "Test Message"};
var data = new AES<object>(Data);

Console.WriteLine(JsonConvert.SerializeObject(Data));
Console.WriteLine(data.EncryptedMessage);
var decrypt = data.DecryptMessage();
Console.WriteLine(JsonConvert.SerializePBasicObject(decrypt));

var data = new VMData();
data.Load();
data.Values.Add(new(){ Cmd = WTServer.Command.Login, Value = "Bad News", ID = data.Values.Count() + 1});
data.Save();
AES<List<MData>> a = new (data.Values);
Console.WriteLine(JsonConvert.SerializeObject(data.Values));
Console.WriteLine(a.EncryptedMessage); */
UDPServer uDP = new();