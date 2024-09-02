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
        /*public static void ClientStartInThread(string nick)
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
        }  */
        internal static async Task SendMessage(string name)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0);
            UdpClient udpClient = new UdpClient();

            while (true)
            {
                Console.WriteLine("Enter message or Exit to exit");

                string message1 = Console.ReadLine();

                if (String.IsNullOrEmpty(message1))
                {
                    Console.WriteLine("Message cannot be empty");
                    continue;
                }




                else if (message1.ToLower() == "exit")
                {
                    User userClientToExit = new User(name, message1);
                    userClientToExit.CancellationTokenSource.Cancel();
                    var jsonToExit = userClientToExit.ToJson();
                    await udpClient.SendAsync(System.Text.Encoding.UTF8.GetBytes(jsonToExit), jsonToExit.Length, ep);
                    udpClient.Close();
                    break;
                }
                else
                {
                    User userClient = new User(name, message1);
                    var jsonToS = userClient.ToJson();
                    await udpClient.SendAsync(System.Text.Encoding.UTF8.GetBytes(jsonToS), jsonToS.Length, ep);

                    /* var answer = udpClient.Receive(ref ep);
                     if (answer.Length == 0 || answer == null)
                     {
                         Console.WriteLine("Server is not responding");
                     }
                     else
                     {
                         string answerMessage = System.Text.Encoding.UTF8.GetString(answer);
                         User userServer = User.FromJson(answerMessage);
                         Console.WriteLine(userServer.ToString());
                     }*/ //An existing connection was forcibly closed by the remote host. (Exception from HRESULT: 0x800703E3)

                    var answer = await udpClient.ReceiveAsync();  

                    if (answer.Buffer.Length == 0 || answer.Buffer == null)
                    {
                        Console.WriteLine("Server is not responding");
                    }
                    else
                    {
                        string answerMessage = System.Text.Encoding.UTF8.GetString(answer.Buffer);
                        User userServer = User.FromJson(answerMessage);
                        Console.WriteLine(userServer.ToString());
                    }
                }

            }
        }
    }
}
