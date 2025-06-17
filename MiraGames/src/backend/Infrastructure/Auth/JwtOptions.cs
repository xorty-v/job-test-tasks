namespace backend.Infrastructure.Auth;

public class JwtOptions
{
    public int AccessExpiresHours { get; set; }
    public int RefreshExpiresDays { get; set; }
    public string SecretKey { get; set; }
}