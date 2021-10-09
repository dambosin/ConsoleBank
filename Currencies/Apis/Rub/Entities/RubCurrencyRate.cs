using System.Xml.Serialization;

namespace Currencies.Apis.Rub.Entities
{

    public class RubCurrencyRate
    {
        [XmlElement("ID")]
        public string Id { get; set; }

        [XmlElement("CharCode")]
        public string CharCode { get; set; }

        [XmlElement("Nominal")]
        public int Nominal { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Value")]
        public double Rate { get; set; }
    }
}
