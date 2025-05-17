namespace ClearerPayAuth.Application.Auth;

public class JwtSettings
{
    public string Secret { get; set; }
    public string ValidAudience { get; set; }
    public string securityKey { get; set; }
    public string PublicKey { get; set; }
    public string APIKey { get; set; }
    public string SecretKey { get; set; }
    public int ExpiryInMinutes { get; set; }
    public int RefreshTokenExpiryInDays { get; set; }
    public string ValidIssuer { get; set; }
}

// This class is used to deserialize the JWT settings from the configuration file.