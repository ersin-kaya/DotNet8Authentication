namespace DotNet8Authentication.ResponseTypes;

public class DataResponse<T> : Response, IDataResponse<T>
{
    public DataResponse(bool succeeded, T? data = default, string? message = null, int? statusCode = null) : base(succeeded, message, statusCode)
    {
        Data = data;
    }

    public T? Data { get; }
}