namespace DotNet8Authentication.ResponseTypes;

public interface IDataResponse<T> : IResponse
{
    T Data { get; }
}