using Newtonsoft.Json;
using System;

namespace Currencies.Entities
{
    public class BynCurrencyRateShort
    {
        [JsonProperty("Cur_ID")]
        public int Id { get; set; }

        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [JsonProperty("Cur_OfficialRate")]
        public double Rate { get; set; }
    }
}
