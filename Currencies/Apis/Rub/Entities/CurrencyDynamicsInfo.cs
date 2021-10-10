using System;
using System.Xml.Serialization;

namespace Currencies.Apis.Rub.Entities
{
    public class CurrencyDynamicsInfo
    {
        [XmlIgnore]
        public DateTime Date { get; set; }

        [XmlElement("Date")]

        public string SerializedDate
        {
            get => Date.ToString("dd.MM.yyyy");
            set => Date = DateTime.Parse(value);
        }

        [XmlElement("Nominal")]
        public int Nominal { get; set; }

        [XmlIgnore]
        public double Rate { get; set; }

        [XmlElement("Value")]
        public string RateSerialized
        {
            get => Rate.ToString("G17");
            set => Rate = double.Parse(value);
        }
    }
}