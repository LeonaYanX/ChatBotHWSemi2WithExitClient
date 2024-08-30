using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ChatBotHWSemi2WithExitClient
{
    internal class Client
    {
        public static void ClientStartInThread(string nick)
        {
           var t = new Thread(()=> { ClientProcess(nick); });
                t.Start();
        }
        public static void ClientProcess(string nick)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
            UdpClient udpClient = new UdpClient();


            while (true)
            {
                Console.WriteLine("Enter the message");
                string message = Console.ReadLine();
                if (String.IsNullOrEmpty(message) || message == "exit" || message == "Exit" || message == "EXIT")
                {
                    udpClient.Close();
                    break;
                }
                else
                {
                    User user = new User(nick, message);

                    var json = user.GetJSON();

                    udpClient.Send(Encoding.UTF8.GetBytes(json), json.Length, iPEndPoint);

                    var buffer = udpClient.Receive(ref iPEndPoint);
                    string jsonReceived = Encoding.UTF8.GetString(buffer);
                    User userReceived = User.GetFromJSON(jsonReceived);
                    Console.WriteLine(userReceived.ToString());

                    if (userReceived != null)
                    {
                        User userClientConfirmation = new User(user.UserName, "Message received");
                        var jsonToSend = userClientConfirmation.GetJSON();
                        var bytesToSend = Encoding.UTF8.GetBytes(jsonToSend);
                        udpClient.Send(bytesToSend, bytesToSend.Length, iPEndPoint);
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong , user is null");
                    }

                }
            }
        }  
    }
}
