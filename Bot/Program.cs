using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using ConsoleApp29.Telegram;
using System.Data;
using System.Net.Http;
using System.Xml;
using System.Net;
using System.Data.SqlTypes;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using ConsoleApp29.XML;
using System.Reflection.Metadata;
using System.Collections.Generic;

namespace ConsoleApp29
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            var botClient = new TelegramBotClient("6678052581:AAGdpvOwLxUKaL450NKJXW2p3_rejbMYYTc");
            botClient.StartReceiving(MethotsTel.Update, MethotsTel.Error);
            Console.Read();
            Console.WriteLine("Всё!");
        }
        
    }

}
