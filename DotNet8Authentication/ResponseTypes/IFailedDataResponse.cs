namespace DotNet8Authentication.ResponseTypes;

public interface IFailedDataResponse<TData> : IDataResponse<TData>, IFailedResponse
{
    
}

public interface IFailedDataResponse<TData, TError> : IDataResponse<TData>, IFailedResponse<TError>
{
    
}