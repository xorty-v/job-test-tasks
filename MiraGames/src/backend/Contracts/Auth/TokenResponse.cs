namespace backend.Contracts.Auth;

public record TokenResponse(string AccessToken, string RefreshToken);