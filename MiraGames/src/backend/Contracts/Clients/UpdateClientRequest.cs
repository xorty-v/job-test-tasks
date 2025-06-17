using System.ComponentModel.DataAnnotations;

namespace backend.Contracts.Clients;

public record UpdateClientRequest(
    [Required] string Name,
    [Required] string Email,
    [Required] decimal BalanceT
);