using HomeAccounting.Application.Interfaces.Infrastructure;
using HomeAccounting.Domain;
using HomeAccounting.Domain.Currency;

namespace HomeAccounting.Infrastructure.Services
{
    public class CurrencyService : ICurrencyService
    {
        public async Task<СurrencyСonversion> GetCurrencyResponseAsync(List<Rates> rates, Purchase purchase)
        {
            

            var conversion = new СurrencyСonversion();

            ///
            // Сделано так, чтобы не блокировать поток.
            // Поскольку в логике нет ничего асинхронного, используем такой прикол. 
            ///

            await Task.Run(() =>
            {
                var ratesCompare = new Dictionary<string, decimal>();

                foreach (var rate in rates)
                {
                    decimal convert = purchase.Price / rate.Cur_OfficialRate ?? 0;
                    ratesCompare.Add(rate.Cur_Name, Math.Round(convert, 2));
                }

                conversion.amountMoneySpent = purchase.Price;
                conversion.moneySpentOn = purchase.Category.Name;
                conversion.whoMadePurchase = purchase.FamilyMember.Name;
                conversion.purchaseComment = purchase.Comment;
                conversion.currencyConversion = ratesCompare;

            });

            return conversion;
        }
    }
}
