using NemoTravel.FinanceCore.Entities;

namespace NemoTravel.FinanceCore.Services;

public class MoneyOperationService : IMoneyOperationService
{
    private readonly ICurrencyConverterService _currencyConverterService;

    public MoneyOperationService(ICurrencyConverterService currencyConverterService)
    {
        _currencyConverterService = currencyConverterService;
    }

    public async Task<Money> Add(Money money1, Money money2)
    {
        if (money1.Currency != money2.Currency)
        {
            money2 = await _currencyConverterService.Convert(money2, money1.Currency);
        }

        return new Money(money1.Amount + money2.Amount, money1.Currency);
    }

    public async Task<Money> Subtract(Money money1, Money money2)
    {
        if (money1.Currency != money2.Currency)
        {
            money2 = await _currencyConverterService.Convert(money2, money1.Currency);
        }

        return new Money(money1.Amount - money2.Amount, money1.Currency);
    }
}