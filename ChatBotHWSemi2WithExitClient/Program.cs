using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotHWSemi2WithExitClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Please enter your name: ");

           string nick = Console.ReadLine();

            while (String.IsNullOrEmpty(nick))
            {
                Console.WriteLine("Please enter your name: ");
                nick = Console.ReadLine();
            }
            Client.ClientStartInThread(nick);
        }
    }
}
