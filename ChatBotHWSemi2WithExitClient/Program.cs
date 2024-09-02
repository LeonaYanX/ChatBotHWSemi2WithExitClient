using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotHWSemi2WithExitClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Please enter your name: ");

           string nickName = Console.ReadLine();

            while (String.IsNullOrEmpty(nickName))
            {
                Console.WriteLine("Please enter your name: ");
                nickName = Console.ReadLine();
            }


           await Client.SendMessage(nickName);
        }
    }
}
