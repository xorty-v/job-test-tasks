using System.ComponentModel.DataAnnotations;

namespace backend.Contracts.Clients;

public record AddClientRequest(
    [Required] string Name,
    [Required] string Email
);