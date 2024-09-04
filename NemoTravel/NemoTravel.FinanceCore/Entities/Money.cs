namespace NemoTravel.FinanceCore.Entities;

public class Money
{
    public decimal Amount { get; }
    public string Currency { get; }


    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public override string ToString()
    {
        return $"{Amount} - {Currency}";
    }
}