using System;
using ConsoleApp29.XML;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Linq;
using ConsoleApp29._Db;
using ConsoleApp29.Telegram.Buttons;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using System.Diagnostics;
using ConsoleApp29.Telegram.Entity;
using ConsoleApp29.XML.Entity;

namespace ConsoleApp29.Telegram
{
    public class MethotsTel
    {
        private static Dictionary<long, My_client> _olineClient = new Dictionary<long, My_client>();
        private static List<string> _list_currens_id = new List<string>();
        private static string url = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=";
        static long Id;
        static string  username;
        static string  nick;
        public static async Task Update(ITelegramBotClient bot, Update update, CancellationToken token)
        {
            


            if (update.CallbackQuery != null)
            {
                 Id = update.CallbackQuery.From.Id;
                 username = update.CallbackQuery.From.Username;
                 nick = update.CallbackQuery.From.FirstName;
                if (!_olineClient.ContainsKey(Id)) await _clientReg();
                if (_olineClient[Id].UserStat == State.Cab)
                {                   
                    switch (update.CallbackQuery.Data)
                    {
                        case "/Back":
                            _olineClient[Id].UserStat = State.Settings;
                            await PSettings(bot);
                            return;
                    }
                    return;

                }    
                if (_olineClient[Id].UserStat == State.Cource)
                {
                    switch (update.CallbackQuery.Data)
                    {
                        case "/Back":
                            _olineClient[Id].UserStat = State.Aunteficated;
                            await PStart(bot);
                            return;
                    }
                }
                if (_olineClient[Id].UserStat == State.Rem)
                {
                    switch (update.CallbackQuery.Data)
                    {
                        case "/Back":
                            _olineClient[Id].UserStat = State.Settings;
                            await PSettings(bot);
                            return;
                    }
                    if (_olineClient[Id].ListCur.Exists(x => x == update.CallbackQuery.Data))
                    {
                        _olineClient[Id].ListCur.Remove(update.CallbackQuery.Data);
                        await bot.SendTextMessageAsync(Id, "Удалил!");
                        return;
                    }
                    else
                    {

                        await bot.SendTextMessageAsync(Id, "Этой валюты нет в вашем списке!");
                        return;
                    }

                    
                }
                if (_olineClient[Id].UserStat == State.Add)
                {

                    switch (update.CallbackQuery.Data)
                    {                                        
                        case "/Back": _olineClient[Id].UserStat = State.Settings;
                            await PSettings(bot);
                            return;
                    }
                    if (_olineClient[Id].ListCur.Exists(x => x == update.CallbackQuery.Data))
                    {
                        await bot.SendTextMessageAsync(Id, "Эта валюта уже добавленна!");
                        return;
                    }
                    else
                    {
                        _olineClient[Id].ListCur.Add(update.CallbackQuery.Data);
                        await bot.SendTextMessageAsync(Id, "Добавил!");
                        return;
                    }
                }
                if (_olineClient[Id].UserStat == State.Settings)
                {
                    switch (update.CallbackQuery.Data)
                    {
                        case "/Add": await PAdd(bot);  break;
                        case "/Rem": await PRem(bot); break;
                        case "/Cab": await PCab(bot); break;
                        case "/Save": await PSave(bot); await PStart(bot); break;
                        case "/Back": await PStart(bot); break;
                    }
                }

                if (_olineClient[Id].UserStat == State.Aunteficated)
                {
                    switch (update.CallbackQuery.Data)
                    {
                        case "/Cource":
                            await PCourse(bot);
                            break;

                        case "/Settings":
                            await PSettings(bot);
                            _olineClient[Id].UserStat = State.Settings;
                            break;

                    }
                    return;
                }

            }
            else 
            {
                Id = update.Message.Chat.Id;
                username = update.Message.Chat.Username;
                nick = update.Message.Chat.FirstName;
                var message = update.Message;
                if (!_olineClient.ContainsKey(Id)) await _clientReg();
                if (_olineClient[Id].UserStat == State.Aunteficated)
                {
                    switch (message.Text)
                    {
                        case "/start": await PStart(bot); break;

                    }
                }
 
            }









            
            
        }



        public static Task Error(ITelegramBotClient botclient, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
        public static async Task PStart( ITelegramBotClient bot){

            await bot.SendTextMessageAsync(Id,"Привет, я бот валют. Вот что я умею", replyMarkup: ButtonMeneger.BforAuntefic());
            _olineClient[Id].ListCur = _Db_Rep.GetTxtInfo(username);
            _olineClient[Id].UserStat = State.Aunteficated;

            return;
        }
        public static async Task PCourse(ITelegramBotClient bot)
        {
            url = MainMethod.GetConnectionString(url);
            List<string> list_curence = _Db_Rep.GetTxtInfo(username);
            List<MoneyModel> list = MainMethod.GetInfo(url, list_curence);
            string messange = $"🧐А вот и актуальный курс, как там Сингапурский доллар?🤔\n\n\n";
            string.Format(messange, Environment.NewLine);
            foreach (var item in list)
            {
                messange += $"🔖Имя валюты {item.Name}🔖\n" +
                                $"📔Код валюты {item.CharCode}📔,\n" +
                                $"💵Номинал валюты {item.Nominal}💵,\n" +
                                $"🆔Айди валюты - {item.Id}🆔\n" +
                                $"💲Цена за одну единицу {item.Value}💲\n\n\n";

            }
            await bot.SendTextMessageAsync(Id, messange, replyMarkup:ButtonMeneger.BforCourse());
            _olineClient[Id].UserStat = State.Cource;
            return;

        }
        public static async Task PAdd(ITelegramBotClient bot)
        {
          await bot.SendTextMessageAsync(Id, "Вот мои валюты выбирай😏!", replyMarkup: ButtonMeneger.BforAdd());            
            _olineClient[Id].UserStat = State.Add;

        }
        public static async Task PRem(ITelegramBotClient bot)
        {
            await bot.SendTextMessageAsync(Id, "Вот твои валюты выбирай😏!", replyMarkup: ButtonMeneger.BforRem(_olineClient[Id].ListCur));
            _olineClient[Id].UserStat = State.Rem;

        }
        public static async Task PCab(ITelegramBotClient bot)
        {
            await bot.SendTextMessageAsync(Id, "🏠Список валют твоего аккаунта🏠", replyMarkup: ButtonMeneger.BforRem(_olineClient[Id].ListCur));
            _olineClient[Id].UserStat = State.Cab;

        }
        public static async Task PSettings( ITelegramBotClient bot)
        {              
            await bot.SendTextMessageAsync(Id, "-----------------------------------🫲🏻Выбирай!🫱🏻-----------------------------------", replyMarkup: ButtonMeneger.BforSettings());
            
            _olineClient[Id].UserStat = State.Settings;

        }
        public static async Task PSave(ITelegramBotClient bot)
        {
            _Db_Rep.SetTxtInfo(username, _olineClient[Id].ListCur);
            await bot.SendTextMessageAsync(Id, "🛠Настройки сохранил🛠");
            _olineClient[Id].UserStat = State.Aunteficated;
        }
        private static async Task _clientReg()
        {
            await _Db_Rep.Registration(Id.ToString(), username, nick);
            Dictionary<int, List<string>> list = MainMethod.MakeCurrence();
            foreach (var item in list)
            {
                item.Value[1] = item.Value[1].Trim(' ').Trim();
                _list_currens_id.Add($"/{item.Value[1]}");
            }
            My_client client = new My_client() {UserStat = State.Aunteficated, ListCur = _Db_Rep.GetTxtInfo(username) };
            _olineClient.Add(Id, client);
        }
        
        

    }
}
