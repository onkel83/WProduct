using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.IO;
using System.Threading.Tasks;

namespace WPBasic
{
    public abstract class ServerReceiver{

    }
    public abstract class ServerReceiver<T>:ServerReceiver{
        private readonly string serverIp = "0.0.0.0"; // Verwende 0.0.0.0 für alle verfügbaren Interfaces
    private readonly int port = 8080;

    public void RunSendAndReceiveTasks(List<T> dataToSend)
    {
        Task senderTask = Task.Run(() => SendEncryptedJsonList(dataToSend));
        Task receiverTask = Task.Run(() => ReceiveEncryptedJsonList());

        Task.WaitAll(senderTask, receiverTask);
    }

    private void SendEncryptedJsonList(List<T> dataToSend)
    {
        string jsonData = JsonConvert.SerializeObject(dataToSend);
        string encryptedData = EncryptData(jsonData);

        using (Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {
            sender.Connect(serverIp, port);

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

    private List<T> ReceiveEncryptedJsonList()
    {
        using (Socket receiver = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {
            receiver.Bind(new IPEndPoint(IPAddress.Any, port)); // Bindung an alle verfügbaren Interfaces

            receiver.Listen(10);

            Socket handler = receiver.Accept();

            byte[] data = new byte[1024];
            int bytesReceived = handler.Receive(data);
            string encryptedData = Encoding.UTF8.GetString(data, 0, bytesReceived);

            string decryptedData = DecryptData(encryptedData);

            List<T> dataList = JsonConvert.DeserializeObject<List<T>>(decryptedData);

            handler.Send(Encoding.UTF8.GetBytes("Data received and processed"));
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();

            return dataList;
        }
    }

    private string EncryptData(string data)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes("yourEncryptionKey");
            aes.IV = Encoding.UTF8.GetBytes("yourEncryptionIV");

            ICryptoTransform encryptor = aes.CreateEncryptor();

            byte[] encryptedBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                    cs.Write(dataBytes, 0, dataBytes.Length);
                }
                encryptedBytes = ms.ToArray();
            }

            return Convert.ToBase64String(encryptedBytes);
        }
    }

    private string DecryptData(string encryptedData)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes("yourEncryptionKey");
            aes.IV = Encoding.UTF8.GetBytes("yourEncryptionIV");

            ICryptoTransform decryptor = aes.CreateDecryptor();

            byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
            string decryptedData;
            using (MemoryStream ms = new MemoryStream(encryptedBytes))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(cs))
                    {
                        decryptedData = reader.ReadToEnd();
                    }
                }
            }

            return decryptedData;
        }
    }
    }
}