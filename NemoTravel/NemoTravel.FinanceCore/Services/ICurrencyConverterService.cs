using NemoTravel.FinanceCore.Entities;

namespace NemoTravel.FinanceCore.Services;

public interface ICurrencyConverterService
{
    public Task<Money> Convert(Money money, string toCurrency);
}