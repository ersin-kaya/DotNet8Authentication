using DotNet8Authentication.Constants;

namespace DotNet8Authentication.Results;

public class ServiceResult : IServiceResult
{
    public bool Succeeded { get; }
    public string? Message { get; }
    public IEnumerable<string>? ErrorMessages { get; }

    protected ServiceResult(bool succeeded, string? message = null, IEnumerable<string>? errorMessages = null)
    {
        Succeeded = succeeded;
        Message = message ?? (succeeded ? Messages.OperationSuccess : Messages.OperationFailed);
        ErrorMessages = succeeded ? null : errorMessages ?? new List<string>();
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
    
    public static new ServiceResult<T> Success(string? message = null)
    {
        return new ServiceResult<T>(succeeded:true, data:default, message:message);
    }

    public static new ServiceResult<T> Failure(IEnumerable<string> errorMessages, string? message = null)
    {
        return new ServiceResult<T>(succeeded:false, data:default, message:message, errorMessages:errorMessages);
    }

    public static new ServiceResult<T> Failure(string errorMessage, string? message = null)
    {
        return new ServiceResult<T>(succeeded:false, data:default, message:message, errorMessages:new List<string> { errorMessage });
    }
    
    // public static new ServiceResult<T> Failure:
    // In the ServiceResult<T> class, the new keyword is used to hide the methods of the base ServiceResult class, 
    // such as Success and Failure. However, this is not necessary because the generic methods in ServiceResult<T> are actually overloads, 
    // not overrides. The new keyword simply suppresses a warning but doesn't alter the method resolution behavior. 
    // This practice is known as method hiding in C#.
}
