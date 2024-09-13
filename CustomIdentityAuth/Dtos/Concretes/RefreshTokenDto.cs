using System.Text.Json.Serialization;

namespace CustomIdentityAuth.Dtos.Concretes;

public class RefreshTokenDto
{
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
}