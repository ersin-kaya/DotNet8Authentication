using System.Text.Json.Serialization;
using CustomIdentityAuth.Dtos.Abstracts;

namespace CustomIdentityAuth.Dtos.Concretes;

public class RefreshTokenDto : ITokenDto
{
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
}