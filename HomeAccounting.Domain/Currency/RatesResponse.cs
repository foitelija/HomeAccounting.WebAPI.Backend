namespace HomeAccounting.Domain.Currency
{
    public class RatesResponse
    {
        public decimal amountMoneySpent { get; set; }
        public string moneySpentOn { get; set; }
        public string? purchaseComment { get; set; }
        public string whoMadePurchase { get; set; }
        public IDictionary<string, decimal> currencyConversion { get; set; }
    }
}
