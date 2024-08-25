namespace DotNet8Authentication.ResponseTypes;

public class Response : IResponse
{
    public Response(bool succeeded, string message = null, int statusCode = 999)
    {
        Succeeded = succeeded;
        Message = message;
        StatusCode = (statusCode != 999)
            ? statusCode
            : (succeeded ? 1000 : 1500);
    }

    public bool Succeeded { get; }
    public string Message { get; }
    public int StatusCode { get; }
}