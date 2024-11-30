namespace Roboline.Domain.Contracts;

public record ProductCategoryRequest(string Name, string Description) : NameDescriptionRequest(Name, Description);