using System.ComponentModel.DataAnnotations;

namespace backend.Contracts.Auth;

public record LoginRequest(
    [Required] string Email,
    [Required] string Password
);