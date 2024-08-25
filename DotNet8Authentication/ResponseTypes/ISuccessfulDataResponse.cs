namespace DotNet8Authentication.ResponseTypes;

public interface ISuccessfulDataResponse<T> : IDataResponse<T>, ISuccessfulResponse
{
    
}