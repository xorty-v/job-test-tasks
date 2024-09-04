using NemoTravel.FinanceCore.Entities;
using NemoTravel.FinanceCore.Infrastructure;

namespace NemoTravel.FinanceCore.Services;

public class CurrencyConverterService : ICurrencyConverterService
{
    private readonly string[] _intermediateCurrencies = { "USD", "EUR" };
    private readonly IExchangeRateProvider _exchangeRateProvider;

    public CurrencyConverterService(IExchangeRateProvider exchangeRateProvider)
    {
        _exchangeRateProvider = exchangeRateProvider;
    }

    public async Task<Money> Convert(Money money, string toCurrency)
    {
        if (money.Currency == toCurrency)
        {
            return money;
        }

        var exchangeRate = await _exchangeRateProvider.GetExchangeRate(money.Currency, toCurrency);

        if (exchangeRate == null)
        {
            foreach (var intermediateCurrency in _intermediateCurrencies)
            {
                var rateToInterm = await _exchangeRateProvider.GetExchangeRate(money.Currency, intermediateCurrency);

                var rateFromInterm = await _exchangeRateProvider.GetExchangeRate(intermediateCurrency, toCurrency);

                if (rateToInterm != null && rateFromInterm != null)
                {
                    exchangeRate = rateToInterm!.Value * rateFromInterm!.Value;
                    break;
                }
            }
        }

        return new Money(money.Amount * exchangeRate.Value, toCurrency);
    }
}