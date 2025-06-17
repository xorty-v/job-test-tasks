namespace backend.Contracts.Payments;

public record PaymentResponse(string ClientEmail, decimal Amount, DateTime TimestampOnUtc);