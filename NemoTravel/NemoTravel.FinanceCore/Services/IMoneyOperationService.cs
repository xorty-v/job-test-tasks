using NemoTravel.FinanceCore.Entities;

namespace NemoTravel.FinanceCore.Services;

public interface IMoneyOperationService
{
    public Task<Money> Add(Money money1, Money money2);
    public Task<Money> Subtract(Money money1, Money money2);
}