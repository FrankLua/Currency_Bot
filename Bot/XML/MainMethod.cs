using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Reflection.PortableExecutable;
using static System.Net.WebRequestMethods;
using ConsoleApp29.XML.Entity;

namespace ConsoleApp29.XML
{
    public class MainMethod
    {
        public static List<MoneyModel> GetInfo(string url, List<string> list_currency)
        {

            string xml;
            using (var webClient = new WebClient() { Encoding = Encoding.UTF8 })
            {


                byte[] reply = webClient.DownloadData(url);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding1251 = Encoding.GetEncoding("windows-1251");
                var convertedBytes = Encoding.Convert(encoding1251, Encoding.UTF8, reply);

                xml = Encoding.UTF8.GetString(convertedBytes);
                
            }
            XDocument doc = XDocument.Parse(xml);

            var Xdoc = ToXmlDocument(doc);

            XmlElement kol = Xdoc.DocumentElement;
            List<MoneyModel> money = new List<MoneyModel>();
            foreach (XmlNode node in kol)
            {
                if (node.ChildNodes.Count > 2 && list_currency.Exists(x => x == node.Attributes[0].Value))
                {
                    MoneyModel moneyModel = new MoneyModel();
                    moneyModel.RId = node.Attributes[0].Value;
                    foreach (XmlNode node2 in node)
                    {
                        switch (node2.Name)
                        {
                            case "NumCode":
                                moneyModel.Id = int.Parse(node2.InnerText);


                                break;
                            case "CharCode":
                                moneyModel.CharCode = node2.InnerText;


                                break;
                            case "Nominal":
                                moneyModel.Nominal = int.Parse(node2.InnerText);


                                break;
                            case "Name":
                                moneyModel.Name = node2.InnerText;


                                break;
                            case "Value":
                                moneyModel.Value = Decimal.Parse(node2.InnerText);


                                break;
                        }
                        





                    }
                    money.Add(moneyModel);
                }
            }
            return money;

        }

        public static XmlDocument ToXmlDocument(XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;



        }
        public static string GetConnectionString(string url)
        {            
            return url += $"{29}/{(DateTime.Now.Month+1).ToString().Insert(0,"0")}/{DateTime.Now.Year}";
        }
        public static Dictionary<int, List<string>> MakeCurrence()
        {
            string url = "https://cbr.ru/scripts/XML_val.asp?d=0";
            string xml;
            using (var webClient = new WebClient() { Encoding = Encoding.UTF8 })
            {


                byte[] reply = webClient.DownloadData(url);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding1251 = Encoding.GetEncoding("windows-1251");
                var convertedBytes = Encoding.Convert(encoding1251, Encoding.UTF8, reply);

                xml = Encoding.UTF8.GetString(convertedBytes);

            }
            XDocument doc = XDocument.Parse(xml);

            var Xdoc = ToXmlDocument(doc);

            XmlElement kol = Xdoc.DocumentElement;

            Dictionary<int,List<string>> Currence = new Dictionary<int, List<string>>();
            int i = 1;
            foreach (XmlNode item in kol)
            {
                if(item.ChildNodes.Count > 2)
                {
                    List<string> list = new List<string>() { item.ChildNodes[0].InnerText, item.ChildNodes[3].InnerText };                    
                    Currence.Add(i,list); 
                }
                i++;
            }
            
            
            return Currence;

        }
        public static List<MoneyModel> MakeAllStat(string url)
        {

            string xml;
            using (var webClient = new WebClient() { Encoding = Encoding.UTF8 })
            {


                byte[] reply = webClient.DownloadData(url);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding1251 = Encoding.GetEncoding("windows-1251");
                var convertedBytes = Encoding.Convert(encoding1251, Encoding.UTF8, reply);

                xml = Encoding.UTF8.GetString(convertedBytes);

            }
            XDocument doc = XDocument.Parse(xml);

            var Xdoc = ToXmlDocument(doc);

            XmlElement kol = Xdoc.DocumentElement;
            List<MoneyModel> money = new List<MoneyModel>();
            foreach (XmlNode node in kol)
            {
                if (node.ChildNodes.Count > 2)
                {
                    MoneyModel moneyModel = new MoneyModel();
                    moneyModel.RId = node.Attributes[0].Value;
                    foreach (XmlNode node2 in node)
                    {
                        switch (node2.Name)
                        {
                            case "NumCode":
                                moneyModel.Id = int.Parse(node2.InnerText);


                                break;
                            case "CharCode":
                                moneyModel.CharCode = node2.InnerText;


                                break;
                            case "Nominal":
                                moneyModel.Nominal = int.Parse(node2.InnerText);


                                break;
                            case "Name":
                                moneyModel.Name = node2.InnerText;


                                break;
                            case "Value":
                                moneyModel.Value = Decimal.Parse(node2.InnerText);


                                break;
                        }






                    }
                    money.Add(moneyModel);
                }
            }
            return money;

        }

    }
}
