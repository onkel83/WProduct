using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace WPBasic
{
    public abstract class ServerReceiver{
        public readonly string _ServerIP = Settings.GetSetting("ServerIP");
        public readonly int _Port = Convert.ToInt32(Settings.GetSetting("Port"));
    }
    public class ServerReceiver<T>:ServerReceiver{
        public void RunSendAndReceiveTasks(List<T> dataToSend){
            Task senderTask = Task.Run(() => SendEncryptedJsonList(dataToSend));
            Task receiverTask = Task.Run(() => ReceiveEncryptedJsonList());

            Task.WaitAll(senderTask, receiverTask);
        }

        private void SendEncryptedJsonList(List<T> dataToSend){
            string jsonData = JsonConvert.SerializeObject(dataToSend);
            string encryptedData = jsonData;
            using (Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)){
                sender.Connect(_ServerIP, _Port);

                byte[] bytes = Encoding.UTF8.GetBytes(encryptedData);
                sender.Send(bytes);

                byte[] data = new byte[1024];
                int bytesReceived = sender.Receive(data);
                string response = Encoding.UTF8.GetString(data, 0, bytesReceived);
                Console.WriteLine("Response received: " + response);

                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
        }

        private List<T> ReceiveEncryptedJsonList(){
            using (Socket receiver = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)){
                receiver.Bind(new IPEndPoint(IPAddress.Any, _Port)); // Bindung an alle verfügbaren Interfaces

                receiver.Listen(10);

                Socket handler = receiver.Accept();

                byte[] data = new byte[1024];
                int bytesReceived = handler.Receive(data);
                string encryptedData = Encoding.UTF8.GetString(data, 0, bytesReceived);

                string decryptedData = encryptedData;

                List<T> dataList = JsonConvert.DeserializeObject<List<T>>(decryptedData);

                handler.Send(Encoding.UTF8.GetBytes("Data received and processed"));
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

                return dataList;
            }
        }
    }
}