using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp29._Db
{
    public class _Db_Rep
    {
        public static SQLiteConnection _Db;

        public static async Task Registration(string chatId, string userName, string NickName)
        {
            try
            {
               if (await UserCheak(userName))
                {
                    Console.WriteLine($"Person Login. UserName:{userName}, ChatId:{chatId}, UserNickName:{NickName}");
                    return;
                }
                    _Db = new SQLiteConnection("data source=Db.db");
                    _Db.Open();
                    SQLiteCommand cmd = _Db.CreateCommand();
                    cmd.CommandText = "INSERT INTO Users VALUES(@ChatId,@UserName,@NIckName)";
                    cmd.Parameters.AddWithValue("@ChatId", chatId);
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@NIckName", NickName);

                    await cmd.ExecuteNonQueryAsync();
                    _Db.Close();
                string path = $"root/{userName}/settings";
                List<string> list = new List<string>() { "R01235", "R01239" };
                Directory.CreateDirectory(path);               
                File.Create(path+"/current.txt").Close();                
                using (StreamWriter sw = File.CreateText(path + "/current.txt"))
                {
                    byte i = 0;
                    foreach(string item in list)
                    {
                        
                        if (i+1 == list.Count){
                            sw.Write(item);
                            break;
                        }
                        sw.Write(item+"|");
                        i++;
                    }
                }
                Console.WriteLine($"Person registred. UserName:{userName}, ChatId:{chatId}, UserNickName:{NickName}");

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error > " + ex.Message);
            }



        }
        public static List<string> GetTxtInfo(string username)
        {
            try
            {
                string src = $"root/{username}/settings/current.txt";
                string answer = File.ReadAllText(src);
                List<string> list = answer.Split('|').ToList();                
                return list;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
       public static void SetTxtInfo (string username, List<string> list)
        {
            string src = $"root/{username}/settings/current.txt";
            try
            {
                if(File.Exists(src))
                {
                    using (StreamWriter sw = File.CreateText(src))
                    {
                        foreach(string s in list)
                        {
                            sw.Write($"{s}|");
                        }
                        
                    }
                    Console.WriteLine($"Пользователь : {username} поменял свои настройки");
                }

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static async Task<bool> UserCheak( string userName)
        {
            string commandstr = $"SELECT * FROM Users WHERE UserName = '{userName}'";
            using (_Db = new SQLiteConnection("data source=Db.db"))
            {
                _Db.Open();
                SQLiteCommand command = new SQLiteCommand(commandstr, _Db);
                await using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            
            
        }


    }
}
