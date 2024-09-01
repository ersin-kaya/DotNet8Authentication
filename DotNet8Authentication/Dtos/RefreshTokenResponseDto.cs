using System.Text.Json.Serialization;

namespace DotNet8Authentication.Dtos;

public class RefreshTokenResponseDto
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public IEnumerable<string>? ErrorMessages { get; set; } = [];
    
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
    
    [JsonPropertyName("expires_at")]
    public DateTimeOffset ExpiresAt { get; set; }
}