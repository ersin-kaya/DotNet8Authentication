namespace DotNet8Authentication.ResponseTypes;

public class SuccessfulDataResponse<T> : DataResponse<T>, ISuccessfulDataResponse<T> 
{
    public SuccessfulDataResponse(T? data, bool succeeded, string? message = null, int? statusCode = null) : base(data, succeeded, message, statusCode)
    {
        
    }
}