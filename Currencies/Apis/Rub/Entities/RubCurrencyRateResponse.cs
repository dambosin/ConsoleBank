using System;
using System.Xml.Serialization;

namespace Currencies.Apis.Rub.Entities
{
    [XmlRoot("ValCurs")]
    public class RubCurrencyRateResponse
    {
        [XmlAttribute("Date")]
        public DateTime Date { get; set; }

        [XmlElement("Valuta")]
        public RubCurrencyRate[] Items { get; set; }
    }
}
