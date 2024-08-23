namespace DotNet8Authentication.Services.Settings;

public class SettingsService : ISettingsService
{
    public TokenSettings TokenSettings { get; }
    
    public SettingsService(IConfiguration configuration)
    {
        TokenSettings = new TokenSettings
        {
            Audience = configuration["Jwt:Audience"],
            Issuer = configuration["Jwt:Issuer"],
            Key = configuration["Jwt:Key"],
            ExpirationMinutes = int.Parse(configuration["Jwt:ExpirationMinutes"]),
            RefreshTokenExpirationDays = int.Parse(configuration["Jwt:RefreshTokenExpirationDays"])
        };
    }
}