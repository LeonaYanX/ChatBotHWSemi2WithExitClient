using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace ChatBotHWSemi2WithExitClient
{
    public class User
    {
        /* public string UserName { get; set; }
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

         }*/
        [JsonIgnore]
        public CancellationTokenSource CancellationTokenSource { get; set; }

        string Name { get; set; }
        string Text { get; set; }
        DateTime Date { get; set; }

        bool IsCancelled
        {
            get
            {
                return CancellationTokenSource.IsCancellationRequested;
            }
        }

        public User(string name, string text)
        {
            Name = name;
            Text = text;
            Date = DateTime.Now;
            CancellationTokenSource = new CancellationTokenSource();
        }
        public User() { }
        public override string ToString() { return $"{Name}:- {Text} --- {Date}"; }
        internal static User? FromJson(string message)
        {
            return JsonSerializer.Deserialize<User>(message);
        }

        internal string ToJson()
        {
           try
            {
                return JsonSerializer.Serialize(this);
            }
            catch
            {

                Console.WriteLine("Can't parse JSON");
                return String.Empty;
            }
            
                
            

           
        }
    }
}

