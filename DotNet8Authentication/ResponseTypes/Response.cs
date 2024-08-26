using System.Text.Json.Serialization;

namespace DotNet8Authentication.ResponseTypes;

public class Response : IResponse
{
    public Response(bool succeeded, string? message = null, int? statusCode = null)
    {
        Succeeded = succeeded;
        Message = message;
        StatusCode = statusCode ?? (succeeded ? 200 : 500);
    }

    public bool Succeeded { get; }
    public string? Message { get; }
    
    [JsonIgnore]
    public int StatusCode { get; }
}