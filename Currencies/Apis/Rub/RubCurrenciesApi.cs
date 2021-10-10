using Currencies.Apis.Rub.Entities;
using Currencies.Common;
using Flurl;
using Flurl.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Currencies.Apis.Rub
{
    public class RubCurrenciesApi : ICurrenciesApi
    {
        private const string _charCode = "RUB";
        private const string BaseApiUrl = "https://www.cbr.ru/scripts";
        private readonly string CurrenciesApiUrl = $"{BaseApiUrl}/XML_valFull.asp";
        private readonly string CurrencyRateApiUrl = $"{BaseApiUrl}/XML_daily.asp";
        private readonly string CurrencyRateDynamicsApiUrl = $"{BaseApiUrl}/XML_dynamic.asp";

        public async Task<CurrencyRateModel[]> GetDynamics(string charCode, DateTime start, DateTime end)
        {
            var currency = (await GetCurrencies()).Single(x => x.CharCode == charCode);
            var xmlResponse = await CallApi(() => CurrencyRateDynamicsApiUrl
            .SetQueryParams(new
            {
                date_req1 = start.ToString("dd/MM/yyyy"),
                date_req2 = end.ToString("dd/MM/yyyy"),
                VAL_NM_RQ = currency.Id
            }).GetStringAsync());
            var response = XmlUtils.ParseXml<RubCurrencyDynamicsResponse>(xmlResponse);
            return response.Items.Select(item => new CurrencyRateModel
            {
                Id = currency.Id,
                CharCode = charCode,
                Date = item.Date,
                Name = currency.Name,
                Nominal = item.Nominal,
                Rate = item.Rate
            }).ToArray();
        }

        public async Task<CurrencyModel[]> GetCurrencies(DateTime? onDate = null)
        {
            var xmlResponse = await CallApi(() => CurrenciesApiUrl.GetStringAsync());
            var response = XmlUtils.ParseXml<RubCurrenciesRespone>(xmlResponse);
            return response.Items.Select(item => new CurrencyModel
            {
                Id = item.Id,
                CharCode = item.CharCode,
                Name = item.Name
            }).ToArray();
        }

        public async Task<CurrencyRateModel> GetCurrencyRate(string charCode, DateTime? onDate = null)
        {
            if(charCode == _charCode)
            {
                return new CurrencyRateModel
                {
                    Id = "R0000",
                    Name = "Российский рубль",
                    CharCode = _charCode,
                    Nominal = 1,
                    Rate = 1,
                    Date = DateTime.Today
                };
            }
            var xmlResponse = await CallApi(() => CurrencyRateApiUrl.SetQueryParam("date_req", onDate).GetStringAsync());
            var response = XmlUtils.ParseXml<RubCurrencyRateResponse>(xmlResponse);
            var rate = response.Items.Single(item => item.CharCode == charCode);
            return new CurrencyRateModel
            {
                Id = rate.Id,
                Name = rate.Name,
                CharCode = rate.CharCode,
                Nominal = rate.Nominal,
                Rate = rate.Rate,
                Date = response.Date
            };
        }

        private static async Task<T> CallApi<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (FlurlHttpException e) when (e.StatusCode == 404)
            {

                throw new CurrencyNotAvailableException("Currency not available");
            }
        }
    }
}
