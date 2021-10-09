using Newtonsoft.Json;
using System;

namespace Currencies
{
    public class BynCurrency
    {
        [JsonProperty("Cur_ID")]
        public int Id { get; set; }

        [JsonProperty("Cur_Code")]
        public int Code { get; set; }

        [JsonProperty("Cur_Abbreviation")]
        public string Abbreviation { get; set; }

        [JsonProperty("Cur_Name")]
        public string Name { get; set; }

        [JsonProperty("Cur_DateEnd")]
        public DateTime DateEnd { get; set; }
        
        [JsonProperty("Cur_Scale")]
        public int Scale { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Code} - {Abbreviation} - {Name} - {DateEnd}";
        }
    }
}
