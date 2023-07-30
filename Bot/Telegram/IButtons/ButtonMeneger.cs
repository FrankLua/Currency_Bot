using ConsoleApp29._Db;
using ConsoleApp29.XML;
using ConsoleApp29.XML.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleApp29.Telegram.Buttons
{
    public class ButtonMeneger
    {
        private static string url = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=";
        public static IReplyMarkup BforAdd()
        {

            List<string> _list_crrens_id = new List<string>();
            Dictionary<int, List<string>> list = MainMethod.MakeCurrence();
            url = MainMethod.GetConnectionString(url);
            List<MoneyModel> AllModelCurrensu = MainMethod.MakeAllStat(url);
            List<List<InlineKeyboardButton>> keybord = new List<List<InlineKeyboardButton>>()

            {
             new List<InlineKeyboardButton>()
            };
            int i = 0;
            foreach (var itemModel in AllModelCurrensu)
            {
                foreach (var itemCurrensu in list)
                {

                    itemCurrensu.Value[1] = itemCurrensu.Value[1].Trim(' ').Trim();
                    if (itemCurrensu.Value[1] == itemModel.RId)
                    {
                        List<InlineKeyboardButton> buttuns = new List<InlineKeyboardButton>();

                        keybord[i].Add(new InlineKeyboardButton($"💲{itemCurrensu.Value[0]}💲") { CallbackData = $"{itemCurrensu.Value[1]}" });
                        if (keybord[i].Count > 1)
                        {
                            keybord.Add(new List<InlineKeyboardButton>());
                            i++;
                        }

                    }

                }

            }
            keybord.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton("Назад") { CallbackData = "/Back" } });
            return new InlineKeyboardMarkup(keybord);
        }
        public static IReplyMarkup BforAuntefic()
        {
            List<List<InlineKeyboardButton>> keyboardButtons = new List<List<InlineKeyboardButton>>();
            List<InlineKeyboardButton> buttonCour = new List<InlineKeyboardButton>()
            {
            new InlineKeyboardButton("Курс") {CallbackData = "/Cource" }            
            };
            List<InlineKeyboardButton> buttonsSett = new List<InlineKeyboardButton>()
            {            
            new InlineKeyboardButton("Настройки") { CallbackData = "/Settings" }
            };
            keyboardButtons.Add(buttonCour);
            keyboardButtons.Add(buttonsSett);
            return new InlineKeyboardMarkup(keyboardButtons);
        }
        public static IReplyMarkup BforSettings()
        {
            List<List<InlineKeyboardButton>> keybord = new List<List<InlineKeyboardButton>>();
            List<InlineKeyboardButton> buttonCur = new List<InlineKeyboardButton>()
            {
            new InlineKeyboardButton("Добавить валюту") {CallbackData = "/Add" },
            new InlineKeyboardButton("Удалить валюту") { CallbackData = "/Rem" }
            };
            List<InlineKeyboardButton> buttonCab = new List<InlineKeyboardButton>()
            {
            new InlineKeyboardButton("Посмотреть какие валюты у меня уже подключенны") {CallbackData = "/Cab" }
            };
            List<InlineKeyboardButton> buttonBack = new List<InlineKeyboardButton>()
            {
            new InlineKeyboardButton("Назад") {CallbackData = "/Back" }
            };
            List<InlineKeyboardButton> buttonSave = new List<InlineKeyboardButton>()
            {
            new InlineKeyboardButton("Сохранить") {CallbackData = "/Save" }
            };
            keybord.Add(buttonCab);
            keybord.Add(buttonCur);
            keybord.Add(buttonSave);
            keybord.Add(buttonBack);

            return new InlineKeyboardMarkup(keybord);
        }
        public static IReplyMarkup BforRem(List<string> userCurlist)
        {

            List<string> _list_crrens_id = new List<string>();
            Dictionary<int, List<string>> list = MainMethod.MakeCurrence();
            url = MainMethod.GetConnectionString(url);
            List<MoneyModel> AllModelCurrensu = MainMethod.MakeAllStat(url);
            List<List<InlineKeyboardButton>> keybord = new List<List<InlineKeyboardButton>>()

            {
             new List<InlineKeyboardButton>()
            };
            int i = 0;
            foreach (var itemModel in AllModelCurrensu)
            {
                foreach (var itemCurrensu in list)
                {

                    itemCurrensu.Value[1] = itemCurrensu.Value[1].Trim(' ').Trim();
                    if (itemCurrensu.Value[1] == itemModel.RId)
                    {
                        if (userCurlist.Exists(x => x == itemCurrensu.Value[1]))
                        {
                            List<InlineKeyboardButton> buttuns = new List<InlineKeyboardButton>();

                            keybord[i].Add(new InlineKeyboardButton($"💲{itemCurrensu.Value[0]}💲") { CallbackData = $"{itemCurrensu.Value[1]}" });
                            if (keybord[i].Count > 1)
                            {
                                keybord.Add(new List<InlineKeyboardButton>());
                                i++;
                            }
                        }
                    }

                }

            }
            keybord.Add(new List<InlineKeyboardButton> { new InlineKeyboardButton("Назад") { CallbackData = "/Back" } });
            return new InlineKeyboardMarkup(keybord);
        }
        public static IReplyMarkup BforCourse()
        {
            List<List<InlineKeyboardButton>> keybord = new List<List<InlineKeyboardButton>>() 
            {
            new List<InlineKeyboardButton>(){ new InlineKeyboardButton("Назад") {CallbackData="/Back" } }            
            };
          
          
            return new InlineKeyboardMarkup(keybord);
        }
    }
}
