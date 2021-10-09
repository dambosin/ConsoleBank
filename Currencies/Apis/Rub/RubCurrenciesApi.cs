using Currencies.Apis.Rub.Entities;
using Currencies.Common;
using Flurl.Http;
using System;
using System.Threading.Tasks;
using System.Linq;
using Flurl;

namespace Currencies.Apis.Rub
{
    public class RubCurrenciesApi : ICurrenciesApi
    {
        private const string BaseApiUrl = "https://www.cbr.ru/scripts";
        private readonly string CurrenciesApiUrl = $"{BaseApiUrl}/XML_valFull.asp";
        private readonly string CurrencyRateApiUrl = $"{BaseApiUrl}/XML_daily.asp";
        private readonly string CurrencyRateDynamicsApiUrl = $"{BaseApiUrl}/rates/dynamics/";

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

        public Task<CurrencyRateModel[]> GetDynamics(string charCode, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
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
