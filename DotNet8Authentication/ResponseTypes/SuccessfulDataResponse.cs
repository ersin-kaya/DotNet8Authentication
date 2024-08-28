namespace DotNet8Authentication.ResponseTypes;

public class SuccessfulDataResponse<T> : DataResponse<T>, ISuccessfulDataResponse<T> 
{
    public SuccessfulDataResponse(T? data = default, string? message = null, int? statusCode = null) : base(true, data, message, statusCode)
    {
        
    }
}