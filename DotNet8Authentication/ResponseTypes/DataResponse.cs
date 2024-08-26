namespace DotNet8Authentication.ResponseTypes;

public class DataResponse<T> : Response, IDataResponse<T>
{
    public DataResponse(T? data, bool succeeded, string? message = null, int? statusCode = null) : base(succeeded, message, statusCode)
    {
        Data = data;
    }

    public T? Data { get; }
}