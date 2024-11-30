namespace Roboline.Domain.Contracts;

public record ProductRequest(string Name, string Description, decimal Price, int CategoryId)
    : NameDescriptionRequest(Name, Description);