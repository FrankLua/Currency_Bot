using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ConsoleApp29.XML.Entity
{
    public class MoneyModel
    {

        [XmlElement("NumCode")]
        public int Id { get; set; }
        [XmlElement("CharCode")]
        public string CharCode { get; set; }
        [XmlElement("Nominal")]
        public int Nominal { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Value")]
        public decimal Value { get; set; }

        public string RId { get; set; }

    }

}
