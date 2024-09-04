namespace NemoTravel.FinanceCore.Infrastructure;

public interface IExchangeRateProvider
{
    public Task<decimal?> GetExchangeRate(string fromCurrency, string toCurrency);
}