namespace backend.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TimestampOnUtc { get; set; }

    public Client Client { get; set; }
}