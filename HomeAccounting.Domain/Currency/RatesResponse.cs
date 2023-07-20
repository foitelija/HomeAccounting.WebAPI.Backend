using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Domain.Currency
{
    public class RatesResponse
    {
        public decimal MoneySpent { get; set; }
        public string SpentForWhat { get; set; }
        public string? SpentComment { get; set; }
        public string NameWhoSpent { get; set; }
        public IDictionary<string, decimal> Currency { get; set; }
    }
}
