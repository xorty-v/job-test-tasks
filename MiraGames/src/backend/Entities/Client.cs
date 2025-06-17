namespace backend.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public decimal BalanceT { get; set; }

    public ICollection<Payment> Payments { get; set; }
}