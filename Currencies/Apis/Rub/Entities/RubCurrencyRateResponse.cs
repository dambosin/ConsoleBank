﻿using System;
using System.Xml.Serialization;

namespace Currencies.Apis.Rub.Entities
{
    [XmlRoot("ValCurs")]
    public class RubCurrencyRateResponse
    {
        [XmlIgnore]
        public DateTime Date { get; set; }

        [XmlElement("Date")]
        public string SerializedDate
        {
            get => Date.ToString("dd.MM.yyyy");
            set => Date = DateTime.Parse(value);
        }

        [XmlElement("Valute")]
        public RubCurrencyRate[] Items { get; set; }
    }
}
