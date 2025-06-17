namespace backend.Entities;

public class Rate
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public DateTime UpdatedAtOnUtc { get; set; }
}