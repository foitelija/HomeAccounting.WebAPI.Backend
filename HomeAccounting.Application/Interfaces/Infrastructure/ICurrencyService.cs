using HomeAccounting.Domain;
using HomeAccounting.Domain.Currency;
using HomeAccounting.Domain.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Interfaces.Infrastructure
{
    public interface ICurrencyService
    {
        Task<СurrencyСonversion> GetCurrencyResponseAsync(int curr_Id, PurchaseList purchase);
    }
}
