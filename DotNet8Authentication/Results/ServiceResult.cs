namespace DotNet8Authentication.Results;

public class ServiceResult : IServiceResult
{
    public bool Succeeded { get; }
    public string? Message { get; }
    public IEnumerable<string>? ErrorMessages { get; }

    protected ServiceResult(bool succeeded, string? message = null, IEnumerable<string>? errorMessages = null)
    {
        Succeeded = succeeded;
        Message = message;
        ErrorMessages = errorMessages;
    }

    public static ServiceResult Success(string? message = null)
    {
        return new ServiceResult(succeeded:true, message:message);
    }

    public static ServiceResult Failure(IEnumerable<string> errorMessages, string? message = null)
    {
        return new ServiceResult(succeeded:false, message:message, errorMessages:errorMessages);
    }

    public static ServiceResult Failure(string errorMessage, string? message = null)
    {
        return new ServiceResult(succeeded:false, message:message, errorMessages:new List<string> { errorMessage });
    }
}

public class ServiceResult<T> : ServiceResult, IServiceResult<T>
{
    public T? Data { get; }

    private ServiceResult(bool succeeded, T? data, string? message = null, IEnumerable<string>? errorMessages = null)
        : base(succeeded, message, errorMessages)
    {
        Data = data;
    }

    public static ServiceResult<T> Success(T data, string? message = null)
    {
        return new ServiceResult<T>(succeeded:true, data:data, message:message);
    }

    public static new ServiceResult<T> Failure(IEnumerable<string> errorMessages, string? message = null)
    {
        return new ServiceResult<T>(succeeded:false, data:default, message:message, errorMessages:errorMessages);
    }

    public static new ServiceResult<T> Failure(string errorMessage, string? message = null)
    {
        return new ServiceResult<T>(succeeded:false, data:default, message:message, errorMessages:new List<string> { errorMessage });
    }
}
