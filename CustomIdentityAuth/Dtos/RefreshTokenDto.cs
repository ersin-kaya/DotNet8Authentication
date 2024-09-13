using System.Text.Json.Serialization;

namespace CustomIdentityAuth.Dtos;

public class RefreshTokenDto
{
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
}