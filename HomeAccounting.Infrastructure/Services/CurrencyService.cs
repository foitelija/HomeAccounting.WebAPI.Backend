using HomeAccounting.Application.Interfaces.Infrastructure;
using HomeAccounting.Domain;
using HomeAccounting.Domain.Currency;
using HomeAccounting.Domain.Enums;
using HomeAccounting.Domain.Purchases;
using MediatR;
using Newtonsoft.Json;
using System.Net.Http;

namespace HomeAccounting.Infrastructure.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;

        public CurrencyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<СurrencyСonversion> GetCurrencyResponseAsync(int curr_Id, PurchaseList purchase)
        {

            var rates = new List<Rates>();
            var conversion = new СurrencyСonversion();

            try
            {
                foreach (var curr_ID in Enum.GetValues<RateEnums>())
                {
                    var response = await _httpClient.GetStringAsync($"https://api.nbrb.by/exrates/rates/{(int)curr_ID}");
                    rates.Add(JsonConvert.DeserializeObject<Rates>(response));
                }


                if (curr_Id > 0)
                {
                    var response = await _httpClient.GetStringAsync($"https://api.nbrb.by/exrates/rates/{curr_Id}");
                    rates.Add(JsonConvert.DeserializeObject<Rates>(response));
                }

                var ratesCompare = new Dictionary<string, decimal>();

                foreach (var rate in rates)
                {
                    decimal convert = purchase.Price / rate.Cur_OfficialRate ?? 0;
                    ratesCompare.Add(rate.Cur_Name, Math.Round(convert, 2));
                }

                conversion.amountMoneySpent = purchase.Price;
                conversion.moneySpentOn = purchase.Category.Name;
                conversion.whoMadePurchase = purchase.UserName;
                conversion.purchaseComment = purchase.Comment;
                conversion.currencyConversion = ratesCompare;

            }
            catch (HttpRequestException) { throw; }
            catch (Exception) { throw; }
            return conversion;
        }
    }
}
