using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatBotHWSemi2WithExitClient
{
    internal class User
    {
        public string UserName { get; set; }
        public string TextMessage { get; set; }

        public DateTime DateAndTime { get; set; }

        public User() { }

        public override string ToString()
        {
            return $"{this.UserName} - {DateAndTime.ToShortTimeString()}: {TextMessage}";
        }

        public User(string userName, string textMessage)
        {
            UserName = userName;
            TextMessage = textMessage;
            DateAndTime = DateTime.Now;
        }
        public string GetJSON()
        {

            return JsonSerializer.Serialize(this);
        }

        public static User GetFromJSON(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<User>(json);
            }
            catch
            {
                Console.WriteLine("Can't parse JSON");
                return null;

            }

        }


    }
}

