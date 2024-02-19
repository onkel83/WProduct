using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WTServer;

class UDPClient
{
    private const int serverPort = 8080;
    private const string serverIp = "127.0.0.1"; // Change this to the actual IP address of your server

    static void Main()
    {
        using (var client = new UdpClient())
        {
            var serverEndpoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);

            try
            {
                Console.WriteLine("UDP Client is running...");

                bool isRunning = true;
                while (isRunning)
                {
                    Console.WriteLine("Enter commands in the following format: ID#Command#Values");
                    string input = Console.ReadLine();
                    string[] data = input.Split('#');

                    MData sendData = new MData
                    {
                        ID = int.Parse(data[0]),
                        Cmd = (Command)Enum.Parse(typeof(Command), data[1], true),
                        Value = data[2]
                    };

                    var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(sendData);
                    byte[] bytes = Encoding.UTF8.GetBytes(jsonData);

                    client.Send(bytes, bytes.Length, serverEndpoint);
                    Console.WriteLine($"Sent data to server: {jsonData}");

                    // Check if user wants to quit
                    Console.WriteLine("Enter 'quit' to stop the client:");
                    string quitCommand = Console.ReadLine().Trim();
                    if (quitCommand.ToLower() == "quit")
                    {
                        isRunning = false;
                    }
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
}
