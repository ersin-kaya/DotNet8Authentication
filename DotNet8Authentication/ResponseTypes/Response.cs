using System.Text.Json.Serialization;

namespace DotNet8Authentication.ResponseTypes;

public class Response : IResponse
{
    public Response(bool succeeded, string message, int statusCode) : this(succeeded, message)
    {
        StatusCode = statusCode;
    }
    
    public Response(bool succeeded, string message) : this(succeeded)
    {
        Message = message;
    }
    
    public Response(bool succeeded, int statusCode) : this(succeeded)
    {
        StatusCode = statusCode;
    }
    
    public Response(bool succeeded)
    {
        Succeeded = succeeded;
    }

    public bool Succeeded { get; }
    public string? Message { get; }
    
    [JsonIgnore]
    public int StatusCode { get; }
}