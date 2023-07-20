using HomeAccounting.Domain;
using HomeAccounting.Domain.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Interfaces.Infrastructure
{
    public interface ICurrencyService
    {
        Task<СurrencyСonversion> GetCurrencyResponseAsync(List<Rates> rates, Purchase purchase);
    }
}
