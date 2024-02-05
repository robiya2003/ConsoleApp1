using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ConsoleApp1.JsonDatabazas
{
    public static class BazaClass
    {
        public static string path = @"C:\Users\LENOVO\Desktop\OnlineMarketBookExcam\ConsoleApp1\JsonDatabazas\DataBaza.json";

        public static bool Checking(long id)
        {
            string StringJson = System.IO.File.ReadAllText(path);
            var JsonList = JsonConvert.DeserializeObject<List<Root>>(StringJson);
            foreach (var el in JsonList)
            {
                if (el.chatid == id) { return false; }
            }
            return true;

        }
        public static void Apppend(long id, string n, string p)
        {
            string StringJson = System.IO.File.ReadAllText(path);
            var JsonList = JsonConvert.DeserializeObject<List<Root>>(StringJson);
            JsonList.Add(new Root()
            {
                chatid = id,
                phonenumber = p,
                firstname = n,

            });
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(JsonConvert.SerializeObject(JsonList));
            }
        }
        public static string GetMe(long id)
        {
            string s = "";
            string StringJson = System.IO.File.ReadAllText(path);
            var JsonList = JsonConvert.DeserializeObject<List<Root>>(StringJson);
            foreach (var el in JsonList)
            {
                if (el.chatid == id)
                {
                    s = s + "Chat Id : " + el.chatid + "\nName : " + el.firstname + "\n Phone Number : " + el.phonenumber;
                }
            }
            return s;
        }
    }
}
